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
        Task<ICollection<BattleAction>> PerformAttack(BattleCard srcCard, BattleCard tarCard, ICollection<BattleCard> cards);
        Task<ICollection<BattleAction>> AttackAll(BattleCard srcCard, ICollection<BattleCard> cards);
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
            var nextCard = cards.Where(x => x.Hp != 0).OrderBy(x => x.Time).FirstOrDefault();
            var timeElapsed = nextCard.Time;
            foreach (var card in cards)
            {
                var cardTimeElapsed = timeElapsed;
                var passives = card.ActiveEffects;
                foreach (var passive in passives)
                {
                    switch (passive.Passive)
                    {
                        case Passive.FREEZE:
                            if (cardTimeElapsed < passive.Param2)
                            {
                                cardTimeElapsed -= cardTimeElapsed / passive.Param1;
                            }
                            else
                            {
                                cardTimeElapsed -= (cardTimeElapsed - passive.Param2) - (-(passive.Param2 - cardTimeElapsed) / passive.Param1);
                            }
                            break;
                        case Passive.STUN:
                            if (cardTimeElapsed < passive.Param1)
                            {
                                cardTimeElapsed -= 0;
                            }
                            else
                            {
                                cardTimeElapsed -= -(passive.Param2 - cardTimeElapsed);
                            }
                            break;
                    };
                }
                cardTimeElapsed = Math.Round(cardTimeElapsed, 2, MidpointRounding.AwayFromZero);
                card.Time = Math.Max(card.Time - cardTimeElapsed, 0);
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

        public Task<ICollection<BattleAction>> PerformAttack(BattleCard srcCard, BattleCard tarCard, ICollection<BattleCard> cards)
        {
            if (srcCard.PlayerCard.Card.AtkType == AtkType.HEAL)
            {
                var status = this.PerformHeal(srcCard, tarCard, cards);
                return status;
            }
            ICollection<BattleAction> retList = new List<BattleAction>();
            var cancelStatus = this.ShouldCancelAttack(srcCard, tarCard);
            if (cancelStatus != CancelStatus.NONE)
            {
                return Task.FromResult(retList);
            }
            var dmg = this.CalculateDmg(srcCard, tarCard);
            var extraDmg = this.CalculateExtraDmg(srcCard, tarCard);
            retList = retList.Concat(this.CalculateEffectsOnAttack(srcCard, tarCard, cards, dmg)).ToList();
            retList = retList.Concat(this.ApplyAttackStatus(srcCard, tarCard, dmg)).ToList();
            tarCard.Hp = tarCard.Hp - dmg - extraDmg;
            if (tarCard.Hp < 0) tarCard.Hp = 0;

            var dmgType = tarCard.Hp == 0 ? HealthChange.DEATH : HealthChange.DAMAGE;
            retList.Add(new BattleAction { Number = dmg + extraDmg, Position = tarCard.Position, Type = dmgType });

            return Task.FromResult(retList);
        }

        public Task<ICollection<BattleAction>> PerformHeal(BattleCard srcCard, BattleCard tarCard, ICollection<BattleCard> cards)
        {
            ICollection<BattleAction> retList = new List<BattleAction>();
            var cancelStatus = this.ShouldCancelAttack(srcCard, tarCard);
            if (cancelStatus != CancelStatus.NONE)
            {
                return Task.FromResult(retList);
            }
            tarCard.Hp = Math.Min(tarCard.GetHp(), tarCard.Hp + srcCard.GetAtk());
            retList.Add(new BattleAction { Number = srcCard.GetAtk(), Position = tarCard.Position, Type = HealthChange.HEAL });
            retList = retList.Concat(this.CalculateEffectsOnAttack(srcCard, tarCard, cards, 0)).ToList();

            return Task.FromResult(retList);
        }

        public Task<ICollection<BattleAction>> AttackAll(BattleCard srcCard, ICollection<BattleCard> cards)
        {
            List<BattleCard> targets;
            if (srcCard.Position < 4)
            {
                targets = cards.Where(x => x.Position > 3).ToList();
            }
            else
            {
                targets = cards.Where(x => x.Position < 4).ToList();
            }

            ICollection<BattleAction> retList = new List<BattleAction>();

            foreach (var tarCard in targets)
            {
                retList = new List<BattleAction>(retList.Concat(this.PerformAttack(srcCard, tarCard, cards).Result));
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
                        case Passive.STUN:
                        case Passive.FREEZE:
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
                            tarCard.Time = Math.Round(tarCard.Time, 2, MidpointRounding.AwayFromZero);
                            break;
                        case Passive.POISON:
                        case Passive.BLEED:
                        case Passive.BLAZE:
                        case Passive.FREEZE:
                        case Passive.DOOM:
                        case Passive.STUN:
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

        public ICollection<BattleAction> CalculateEffectsOnAttack(BattleCard srcCard, BattleCard tarCard, ICollection<BattleCard> cards, int dmgDone)
        {
            var actions = new List<BattleAction>();
            var srcEffs = srcCard.ActiveEffects;
            var tarEffs = tarCard.ActiveEffects;
            for (int i = srcEffs.Count - 1; i >= 0; i--)
            {
                var eff = srcEffs[i];
                switch (eff.Passive)
                {
                    case Passive.BLEED:
                        if (eff.Param2 > 0)
                        {
                            this.ApplyDmg(srcCard, (int)eff.Param1);
                            actions.Add(new BattleAction { Number = (int)eff.Param1, Position = tarCard.Position, Type = HealthChange.BLEED });
                        }
                        else
                        {
                            srcEffs.RemoveAt(i);
                        }
                        break;
                    case Passive.BOUNCE:
                        int[] possibleTargetPos;
                        if (srcCard.Position < 4)
                        {
                            possibleTargetPos = cards.Where(x => x.Position > 3).Select(x => x.Position).ToArray();
                        }
                        else
                        {
                            possibleTargetPos = cards.Where(x => x.Position < 4).Select(x => x.Position).ToArray();
                        }
                        var random = new Random();
                        var targetPos = random.Next(0, possibleTargetPos.Length);
                        this.ApplyDmg(cards.FirstOrDefault(x => x.Position == targetPos), (int)eff.Param1);
                        actions.Add(new BattleAction { Number = (int)eff.Param1, Position = tarCard.Position, Type = HealthChange.DAMAGE });
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
                            actions.Add(new BattleAction { Number = (int)eff.Param1, Position = tarCard.Position, Type = HealthChange.BLAZE });
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
