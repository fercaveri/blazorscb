using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurrealCB.Data;
using SurrealCB.Data.Model;

namespace SurrealCB.Server
{
    public interface IBattleService
    {
        Task<ICollection<BattleAction>> PerformAttack(BattleCard srcCard, BattleCard tarCard);
        Task<ICollection<BattleAction>> AttackAll(BattleCard srcCard, ICollection<BattleCard> targets);
        Task<BattleStatus> NextTurn(ICollection<BattleCard> cards);
        Task<BattleEnd> CheckWinOrLose(ICollection<BattleCard> cards, int srcPos);
    }
    public class BattleService : IBattleService
    {
        private readonly SCBDbContext repository;

        public BattleService(SCBDbContext repository)
        {
            this.repository = repository;
        }

        public Task<BattleStatus> NextTurn(ICollection<BattleCard> cards)
        {
            //var globalEffects = List<EffectOAlgo>();
            //TODO: ver efectos de tiempo tipo freeze etc
            var nextCard = cards.Where(x => x.Hp != 0).OrderBy(x => x.Time).FirstOrDefault();
            var timeElapsed = nextCard.Time;
            foreach (var card in cards)
            {
                //TODO: Apply freeze etc
                card.Time = Math.Max(card.Time - timeElapsed, 0);
            }
            nextCard.Time = nextCard.GetSpd();
            var actions = this.CheckNextTurnStatus(cards, timeElapsed);
            var battleStatus = new BattleStatus
            {
                NextPosition = nextCard.Position,
                Cards = cards,
                Actions = actions
            };
            return Task.FromResult(battleStatus);
        }

        public Task<ICollection<BattleAction>> PerformAttack(BattleCard srcCard, BattleCard tarCard)
        {
            ICollection<BattleAction> retList = new List<BattleAction>();
            var cancelStatus = this.ShouldCancelAttack(srcCard, tarCard);
            if (cancelStatus != CancelStatus.NONE)
            {
                return Task.FromResult(retList);
            }
            var dmg = this.CalculateDmg(srcCard, tarCard);
            var extraDmg = this.CalculateExtraDmg(srcCard, tarCard);
            retList = retList.Concat(this.ApplyAttackStatus(srcCard, tarCard, dmg)).ToList();
            retList = retList.Concat(this.CalculateEffectsOnAttack(srcCard, tarCard, dmg)).ToList();
            tarCard.Hp = tarCard.Hp - dmg - extraDmg;
            if (tarCard.Hp < 0) tarCard.Hp = 0;

            var dmgType = tarCard.Hp == 0 ? HealthChange.DEATH : HealthChange.DAMAGE;
            retList.Add(new BattleAction { Number = dmg + extraDmg, Position = tarCard.Position, Type = dmgType });

            return Task.FromResult(retList);
        }

        public Task<ICollection<BattleAction>> AttackAll(BattleCard srcCard, ICollection<BattleCard> targets)
        {
            ICollection<BattleAction> retList = new List<BattleAction>();

            foreach (var tarCard in targets)
            {
                retList = new List<BattleAction>(retList.Concat(this.PerformAttack(srcCard, tarCard).Result));
            }

            return Task.FromResult(retList);
        }

        public Task<BattleEnd> CheckWinOrLose(ICollection<BattleCard> cards, int srcPos)
        {
            if (srcPos > 3)
            {
                if (!cards.Any(x => x.Position < 4 && x.Hp != 0))
                {
                    return Task.FromResult(BattleEnd.LOSE);
                }
            }
            if (srcPos < 4)
            {
                if (!cards.Any(x => x.Position > 3 && x.Hp != 0))
                {
                    return Task.FromResult(BattleEnd.WIN);
                }
            }
            //TODO: DRAW status
            return Task.FromResult(BattleEnd.CONTINUE);
        }

        public ICollection<BattleAction> CheckNextTurnStatus(ICollection<BattleCard> cards, double timeElapsed)
        {
            var actions = new List<BattleAction>();
            foreach (var card in cards)
            {
                for (int i = card.ActiveEffects.Count - 1; i >= 0; i--)
                {
                    var eff = card.ActiveEffects[i];
                    switch (eff.Passive)
                    {
                        case Passive.BLEED:
                        case Passive.BLAZE:
                            eff.Param2 -= timeElapsed;
                            if (eff.Param2 <= 0)
                            {
                                card.ActiveEffects.RemoveAt(i);
                            }
                            break;
                        case Passive.POISON:
                            var remainingTime = timeElapsed;
                            do
                            {
                                var backParam2 = eff.Param2;
                                eff.Param2 -= remainingTime;
                                remainingTime -= backParam2;
                                if (eff.Param2 <= 0)
                                {
                                    actions.Add(new BattleAction { Number = (int)eff.Param1, Position = card.Position, Type = HealthChange.POISON });
                                    eff.Param2 = cards.FirstOrDefault(x => x.Position == eff.FromPosition).GetPassives().FirstOrDefault(x => x.Passive == Passive.POISON).Param2;
                                    this.ApplyDmg(card, (int)eff.Param1);
                                }
                            }
                            while (remainingTime / eff.Param2 > 1);

                            //TODO: el param 3 no se checkea hasta aca, y si es mas el timeElapsed q el param3, podria ejecutar 2 o mas el poison
                            eff.Param3 -= timeElapsed;
                            if (eff.Param3 <= 0)
                            {
                                card.ActiveEffects.RemoveAt(i);
                            }
                            break;
                        case Passive.DOOM:
                            eff.Param1 -= timeElapsed;
                            if (eff.Param1 <= 0)
                            {
                                card.Hp = 0;
                                card.ActiveEffects.RemoveAt(i);
                                actions.Add(new BattleAction { Position = card.Position, Type = HealthChange.DEATH });
                            }
                            break;
                    };
                }
            }
            return actions;
        }

        public CancelStatus ShouldCancelAttack(BattleCard srcCard, BattleCard tarCard)
        {
            var status = CancelStatus.NONE;
            //TODO: status negativos de mi carta
            var passives = tarCard.PlayerCard.GetPassives();
            var random = new Random();
            foreach (var passive in passives)
            {
                switch (passive.Passive)
                {
                    case Passive.DODGE:
                        var res = random.Next(100);
                        if (res <= passive.Param1)
                        {
                            status = CancelStatus.EVADE;
                            return status;
                        }
                        break;
                };
            }
            return status;
        }

        public int CalculateDmg(BattleCard srcCard, BattleCard tarCard)
        {
            var dmg = srcCard.GetAtk();
            var passives = srcCard.PlayerCard.GetPassives();
            var def = tarCard.GetDef();
            foreach (var passive in passives)
            {
                switch (passive.Passive)
                {
                    case Passive.IGNORE_DEF:
                        def = 0;
                        break;
                    case Passive.PIERCING:
                        def = Math.Max(def - (int)passive.Param1, 0);
                        break;
                };
            }
            dmg = this.ApplyElementToDmg(srcCard.PlayerCard.Card.Element, tarCard.PlayerCard.Card.Element, dmg);
            return Math.Max(0, dmg - def);
        }

        public int CalculateExtraDmg(BattleCard srcCard, BattleCard tarCard)
        {
            var dmg = 0;
            var passives = srcCard.PlayerCard.GetPassives();
            foreach (var passive in passives)
            {
                switch (passive.Passive)
                {
                    case Passive.HP_SHATTER:
                        dmg += (int)Math.Ceiling((double)(tarCard.Hp / passive.Param1));
                        break;
                };
            }
            return dmg;
        }

        public ICollection<BattleAction> ApplyAttackStatus(BattleCard srcCard, BattleCard tarCard, int dmgDone)
        {
            var actions = new List<BattleAction>();
            var srcPassives = srcCard.PlayerCard.GetPassives();
            var tarPassives = tarCard.PlayerCard.GetPassives();
            var random = new Random();
            var randNum = random.Next(1, 100);
            if (randNum > tarCard.GetImm())
            {
                foreach (var passive in srcPassives)
                {
                    switch (passive.Passive)
                    {
                        case Passive.BACKTRACK:
                            tarCard.Time = Math.Min(tarCard.GetSpd(), tarCard.Time + passive.Param1);
                            break;
                        case Passive.POISON:
                        case Passive.BLEED:
                        case Passive.BLAZE:
                            var eff = tarCard.ActiveEffects.FirstOrDefault(x => x.Passive == passive.Passive);
                            if (eff != null)
                            {
                                tarCard.ActiveEffects.Remove(eff);
                            }
                            tarCard.ActiveEffects.Add(new ActiveEffect
                            {
                                FromPosition = srcCard.Position,
                                Id = passive.Id,
                                Passive = passive.Passive,
                                Param1 = passive.Param1,
                                Param2 = passive.Param2,
                                Param3 = passive.Param3
                            });
                            break;
                    };
                }
            }
            randNum = random.Next(1, 100);
            if (randNum > srcCard.GetImm())
            {
                foreach (var passive in tarPassives)
                {
                    switch (passive.Passive)
                    {
                        //TODO: spike armor
                    };
                }
            }
            return actions;
        }

        public ICollection<BattleAction> CalculateEffectsOnAttack(BattleCard srcCard, BattleCard tarCard, int dmgDone)
        {
            var actions = new List<BattleAction>();
            var srcEffs = srcCard.ActiveEffects;
            var tarEffs = srcCard.ActiveEffects;
            for (int i = srcEffs.Count - 1; i >= 0; i--)
            {
                var eff = srcEffs[i];
                switch (eff.Passive)
                {
                    case Passive.BLEED:
                        if (eff.Param2 > 0)
                        {
                            this.ApplyDmg(srcCard, (int)eff.Param1);
                        }
                        else
                        {
                            srcEffs.RemoveAt(i);
                        }
                        break;
                };
            }

            for (int i = tarEffs.Count - 1; i >= 0; i--)
            {
                var eff = tarEffs[i];
                switch (eff.Passive)
                {
                    case Passive.BLAZE:
                        if (eff.Param2 > 0)
                        {
                            this.ApplyDmg(tarCard, (int)eff.Param1);
                        }
                        else
                        {
                            srcEffs.RemoveAt(i);
                        }
                        break;
                };
            }
            return actions;
        }

        public int ApplyElementToDmg(Element src, Element tar, int dmg)
        {
            //TODO: hacerlo
            return dmg;
        }

        public void ApplyDmg(BattleCard card, int dmg)
        {
            card.Hp -= dmg;
            if (card.Hp < 0) card.Hp = 0;
        }
    }
}
