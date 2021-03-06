using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate.Linq;
using SurrealCB.Data;
using SurrealCB.Data.Enum;
using SurrealCB.Data.Model;
using SurrealCB.Data.Repository;

namespace SurrealCB.Server
{
    public interface IBattleService
    {
        Task<ICollection<BattleAction>> PerformAttack(BattleCard srcCard, ICollection<BattleCard> cards, int tarPos = -1);
        Task<ICollection<BattleAction>> PerformBasicAttack(BattleCard srcCard, BattleCard tarCard, ICollection<BattleCard> cards);
        Task<ICollection<BattleAction>> AttackAll(BattleCard srcCard, ICollection<BattleCard> cards);
        Task<BattleStatus> NextTurn(ICollection<BattleCard> cards);
        Task<BattleEnd> CheckWinOrLose(ICollection<BattleCard> cards, int srcPos);
        HealthChange GetActionType(Passive p);
    }
    public class BattleService : IBattleService
    {
        private readonly IRepository repository;

        public BattleService(IRepository repository)
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
                        case Passive.COLDING:
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
            var random = new Random();
            for (int i = nextCard.ActiveEffects.Count - 1; i >= 0; i--)
            {
                var randNum = random.Next(0, 100);
                var eff = nextCard.ActiveEffects.ToList()[i];
                switch (eff.Passive)
                {
                    case Passive.ELECTRIFY:
                        if (randNum < eff.Param2)
                        {
                            return this.NextTurn(cards);
                        }
                        break;
                }
            }
            var battleStatus = new BattleStatus
            {
                NextPosition = nextCard.Position,
                Cards = cards,
                Actions = actions
            };
            return Task.FromResult(battleStatus);
        }

        public async Task<ICollection<BattleAction>> PerformAttack(BattleCard srcCard, ICollection<BattleCard> cards, int tarPos = -1)
        {
            var tarCard = cards.Where(x => x.Position == tarPos).FirstOrDefault();
            if (srcCard.PlayerCard.Card.AtkType == AtkType.RANDOM)
            {
                while (tarPos == -1)
                {
                    var random = new Random();
                    int tryPos = -1;
                    List<int> currentPositions;
                    if (srcCard.Position < 4)
                    {
                        currentPositions = cards.Where(x => x.Position > 3 && x.Hp > 0).Select(x => x.Position).ToList();
                    }
                    else
                    {
                        currentPositions = cards.Where(x => x.Position < 4 && x.Hp > 0).Select(x => x.Position).ToList();
                    }
                    tryPos = currentPositions[random.Next(0, currentPositions.Count())];
                    var tryCard = cards.FirstOrDefault(x => x.Position == tryPos);
                    if (tryCard != null)
                    {
                        tarPos = tryCard.Position;
                        tarCard = tryCard;
                    }
                }
            }
            else if (srcCard.PlayerCard.Card.AtkType == AtkType.LOWEST_HP)
            {
                if (srcCard.Position < 4)
                {
                    tarCard = cards.FirstOrDefault(x => x.Position > 3 && x.Hp == cards.Min(x => x.Hp));
                }
                else
                {
                    tarCard = cards.FirstOrDefault(x => x.Position < 4 && x.Hp == cards.Min(x => x.Hp));
                }
            }
            else if (srcCard.PlayerCard.Card.AtkType == AtkType.ALL)
            {
                return await this.AttackAll(srcCard, cards);
            }
            if (srcCard.PlayerCard.Card.AtkType == AtkType.HEAL)
            {
                return await this.PerformHeal(srcCard, tarCard, cards);
            }

            //TODO: Not confussing ALL attacks, should?
            if (srcCard.ActiveEffects.Any(x => x.Passive == Passive.CONFUSSION))
            {
                var rand = new Random();
                var randNum = 0;
                if (srcCard.Position > 3)
                {
                    randNum = rand.Next(4, 8);
                }
                else
                {
                    randNum = rand.Next(0, 4);
                }
                var confussion = srcCard.ActiveEffects.FirstOrDefault(x => x.Passive == Passive.CONFUSSION);
                srcCard.ActiveEffects.Remove(confussion);
                tarCard = cards.FirstOrDefault(x => x.Position == randNum);
            }
            if (tarCard.ActiveEffects.Any(x => x.Passive == Passive.THIEF) || srcCard.ActiveEffects.Any(x => x.Passive == Passive.DEVIATE))
            {
                int[] possibleTargetPos;
                if (srcCard.Position < 4)
                {
                    possibleTargetPos = cards.Where(x => x.Position > 3 && x.Hp != 0 && x.Position != tarCard.Position).Select(x => x.Position).ToArray();
                }
                else
                {
                    possibleTargetPos = cards.Where(x => x.Position < 4 && x.Hp != 0 && x.Position != tarCard.Position).Select(x => x.Position).ToArray();
                }
                if (possibleTargetPos.Length == 0) return new List<BattleAction> { };
                var rand = new Random();
                var pos = possibleTargetPos[rand.Next(0, possibleTargetPos.Length - 1)];
                tarCard = cards.FirstOrDefault(x => x.Position == pos);
            }
            return await this.PerformBasicAttack(srcCard, tarCard, cards);
        }

        public Task<ICollection<BattleAction>> PerformBasicAttack(BattleCard srcCard, BattleCard tarCard, ICollection<BattleCard> cards)
        {
            var random = new Random();
            ICollection<BattleAction> retList = new List<BattleAction>();
            var cancelStatus = this.ShouldCancelAttack(srcCard, tarCard);
            if (cancelStatus != CancelStatus.NONE)
            {
                return Task.FromResult(retList);
            }
            var dmg = this.CalculateDmg(srcCard, tarCard);
            var extraDmg = this.CalculateExtraDmg(srcCard, tarCard, dmg);
            if (tarCard.GetPassives().Any(x => x.Passive == Passive.GHOST))
            {
                dmg = extraDmg = 0;
            }
            var deflect = tarCard.GetPassives().FirstOrDefault(x => x.Passive == Passive.DEFLECT);
            if (deflect != null)
            {
                BattleCard deflectTarget = null;
                var nextNum = random.Next(0, 100);
                if (nextNum < deflect.Param1)
                {
                    if (srcCard.Position < 4)
                    {
                        var allyCount = cards.Count(x => x.Position > 3 && x.Hp > 0 && x.Position != srcCard.Position);
                        if (allyCount > 0)
                        {
                            var targetIndex = random.Next(0, allyCount);
                            deflectTarget = cards.Where(x => x.Position > 3 && x.Hp > 0 && x.Position != srcCard.Position).ToList()[targetIndex];
                        }
                    }
                    else
                    {
                        var allyCount = cards.Count(x => x.Position < 4 && x.Hp > 0 && x.Position != srcCard.Position);
                        if (allyCount > 0)
                        {
                            var targetIndex = random.Next(0, allyCount);
                            deflectTarget = cards.Where(x => x.Position < 4 && x.Hp > 0 && x.Position != srcCard.Position).ToList()[targetIndex];
                        }
                    }
                    if (deflectTarget != null)
                    {
                        return this.PerformBasicAttack(srcCard, deflectTarget, cards);
                    }
                }
            }
            retList = retList.Concat(this.CalculateEffectsOnAttack(srcCard, tarCard, cards, dmg)).ToList();
            retList = retList.Concat(this.ApplyAttackStatus(srcCard, tarCard, dmg, cards)).ToList();
            tarCard.Hp = tarCard.Hp - dmg - extraDmg;
            if (tarCard.Hp < 0) tarCard.Hp = 0;

            var dblAtkPassive = srcCard.GetPassives().FirstOrDefault(x => x.Passive == Passive.DOUBLE_ATTACK);
            if (dblAtkPassive != null && !srcCard.ActiveEffects.Any(x => x.Passive == Passive.DOUBLE_ATTACK))
            {
                var randNum = random.Next(0, 100);
                if (randNum < dblAtkPassive.Param1)
                {
                    var eff = this.DoEff(srcCard.Position, dblAtkPassive.Id, dblAtkPassive.Passive, dblAtkPassive.Param1);
                    srcCard.ActiveEffects.Add(eff);
                    var newActions = this.PerformBasicAttack(srcCard, tarCard, cards).Result;
                    foreach (var act in newActions)
                    {
                        retList.Add(act);
                    }
                    srcCard.ActiveEffects.Remove(eff);
                }
            }

            var dmgType = tarCard.Hp == 0 ? HealthChange.DEATH : HealthChange.DAMAGE;
            retList.Add(new BattleAction { Number = dmg + extraDmg, Position = tarCard.Position, Type = dmgType });
            if (dmgType == HealthChange.DEATH)
            {
                retList = retList.Concat(this.TriggerDeathPassive(tarCard, cards, srcCard.Position)).ToList();
            }

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

            var passives = srcCard.GetPassives();
            if (passives.Any(x => x.Passive == Passive.DISPELL))
            {
                tarCard.ActiveEffects = new List<ActiveEffect>();
            }
            var shield = passives.FirstOrDefault(x => x.Passive == Passive.SHIELD);
            if (shield != null)
            {
                tarCard.Shield += (int)shield.Param1;
                if (tarCard.Shield > tarCard.GetHp() / 2)
                {
                    tarCard.Shield = tarCard.GetHp() / 2;
                }
                retList.Add(new BattleAction { Number = (int)shield.Param1, Position = tarCard.Position, Type = HealthChange.SHIELD });
            }
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
                retList = new List<BattleAction>(retList.Concat(this.PerformBasicAttack(srcCard, tarCard, cards).Result));
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
            var random = new Random();
            foreach (var card in cards)
            {
                for (int i = card.ActiveEffects.Count - 1; i >= 0; i--)
                {
                    var randNum = random.Next(0, 100);
                    var eff = card.ActiveEffects.ToList()[i];
                    switch (eff.Passive)
                    {
                        case Passive.BLEED:
                        case Passive.BLAZE:
                        case Passive.STUN:
                        case Passive.FREEZE:
                        case Passive.COLDING:
                        case Passive.BLIND:
                        case Passive.ELECTRIFY:
                        case Passive.SCORCHED:
                        case Passive.INTENSIFY:
                            eff.Param2 -= timeElapsed;
                            if (eff.Param2 <= 0)
                            {
                                var ret = card.ActiveEffects.ToList();
                                ret.RemoveAt(i);
                                card.ActiveEffects = ret;
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
                                    eff.Param2 = cards.FirstOrDefault(x => x.Position == eff.FromPosition).GetPassives().FirstOrDefault(x => x.Passive == Passive.POISON).Param2;
                                    actions.Add(this.ApplyDmg(card, (int)eff.Param1, eff.Passive));
                                }
                            }
                            while (remainingTime / eff.Param2 > 1);

                            //TODO: el param 3 no se checkea hasta aca, y si es mas el timeElapsed q el param3, podria ejecutar 2 o mas el poison
                            eff.Param3 -= timeElapsed;
                            if (eff.Param3 <= 0)
                            {
                                var ret = card.ActiveEffects.ToList();
                                ret.RemoveAt(i);
                                card.ActiveEffects = ret;
                            }
                            break;
                        case Passive.DOOM:
                            eff.Param1 -= timeElapsed;
                            if (eff.Param1 <= 0)
                            {
                                card.Hp = 0;
                                var ret = card.ActiveEffects.ToList();
                                ret.RemoveAt(i);
                                card.ActiveEffects = ret;
                                //actions.Add(new BattleAction { Position = card.Position, Type = HealthChange.DEATH });
                            }
                            break;
                        case Passive.BURN:
                            eff.Param3 -= timeElapsed;
                            if (eff.Param3 <= 0)
                            {
                                if (randNum < (int)eff.Param1)
                                {
                                    actions.Add(this.ApplyDmg(card, (int)eff.Param2, eff.Passive));
                                }
                                var ret = card.ActiveEffects.ToList();
                                ret.RemoveAt(i);
                                card.ActiveEffects = ret;
                            }
                            break;
                        case Passive.HELLFIRE:
                            eff.Param2 -= timeElapsed;
                            if (eff.Param2 <= 0)
                            {
                                IEnumerable<BattleCard> targets;
                                if (card.Position < 4)
                                {
                                    targets = cards.Where(x => x.Position > 3 && x.Hp > 0);
                                }
                                else
                                {
                                    targets = cards.Where(x => x.Position < 4 && x.Hp > 0);
                                }
                                foreach (var target in targets)
                                {
                                    if (!(card.PlayerCard.Card.Element == Element.FIRE && card.PlayerCard.Card.Element == Element.WATER))
                                    {
                                        actions.Add(this.ApplyDmg(target, (int)eff.Param1, eff.Passive));
                                    }
                                }
                                eff.Param2 = card.GetPassives().FirstOrDefault(x => x.Passive == Passive.HELLFIRE).Param2;
                            }
                            break;
                        case Passive.THIEF:
                        case Passive.DEVIATE:
                            eff.Param1 -= timeElapsed;
                            if (eff.Param1 <= 0)
                            {
                                var ret = card.ActiveEffects.ToList();
                                ret.RemoveAt(i);
                                card.ActiveEffects = ret;
                            }
                            break;
                        case Passive.STRIKER:
                            List<BattleCard> stargets;
                            if (card.Position < 4)
                            {
                                stargets = cards.Where(x => x.Position > 3 && x.Hp > 0).ToList();
                            }
                            else
                            {
                                stargets = cards.Where(x => x.Position < 4 && x.Hp > 0).ToList();
                            }
                            var starget = stargets[random.Next(0, stargets.Count() - 1)];
                            actions.Add(this.ApplyDmg(starget, (int)eff.Param1, eff.Passive));
                            var reteff = card.ActiveEffects.ToList();
                            reteff.RemoveAt(i);
                            card.ActiveEffects = reteff;
                            break;
                        case Passive.INCINERATE:
                            eff.Param2 -= timeElapsed;
                            if (eff.Param2 <= 0)
                            {
                                actions.Add(this.ApplyDmg(card, (int)eff.Param1, eff.Passive));
                                eff.Param2 = cards.FirstOrDefault(x => x.Position == eff.FromPosition).GetPassives().FirstOrDefault(x => x.Passive == Passive.INCINERATE).Param2;
                            }
                            break;
                        case Passive.VENOM:
                            var venomTime = timeElapsed;
                            do
                            {
                                var backParam2 = eff.Param2;
                                eff.Param2 -= venomTime;
                                venomTime -= backParam2;
                                if (eff.Param2 <= 0)
                                {
                                    eff.Param2 = cards.FirstOrDefault(x => x.Position == eff.FromPosition).GetPassives().FirstOrDefault(x => x.Passive == eff.Passive).Param2;
                                    actions.Add(this.ApplyDmg(card, (int)eff.Param1, eff.Passive));
                                    eff.Param1 -= eff.Param3;
                                }

                            }
                            while (venomTime / eff.Param2 > 1 && eff.Param1 > 0);

                            if (eff.Param1 <= 0)
                            {
                                var ret = card.ActiveEffects.ToList();
                                ret.RemoveAt(i);
                                card.ActiveEffects = ret;
                            }
                            break;
                    };
                    if (card.Hp == 0)
                    {
                        actions.Add(new BattleAction { Position = card.Position, Type = HealthChange.DEATH });
                        actions = actions.Concat(this.TriggerDeathPassive(card, cards, eff.FromPosition)).ToList();
                        i = 0;
                    }
                }
                var passives = card.GetPassives();
                foreach (var passive in passives)
                {
                    switch (passive.Passive)
                    {
                        case Passive.BERSEKER:
                            if (card.Hp <= card.GetHp() / 2)
                            {
                                card.ActiveEffects.Add(DoEff(card.Position, passive.Id, passive.Passive, passive.Param1, passive.Param2, passive.Param3));
                            }
                            break;
                    }
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
            foreach (var effect in srcCard.ActiveEffects)
            {
                switch (effect.Passive)
                {
                    case Passive.BLIND:
                        var res = random.Next(100);
                        if (res <= effect.Param1)
                        {
                            status = CancelStatus.MISS;
                            return status;
                        }
                        break;
                };
            }
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
            var tarPassives = tarCard.PlayerCard.GetPassives();
            var def = tarCard.GetDef();
            foreach (var passive in passives)
            {
                switch (passive.Passive)
                {
                    case Passive.DEVIANT:
                        var rand = new Random();
                        dmg = rand.Next(srcCard.GetAtk(), (int)passive.Param1 + 1);
                        break;
                    case Passive.IGNORE_DEF:
                        def = 0;
                        break;
                    case Passive.PIERCING:
                        def = Math.Max(def - (int)passive.Param1, 0);
                        break;
                };
            }
            foreach (var passive in tarPassives)
            {
                switch (passive.Passive)
                {
                    case Passive.TOUGH:
                        var rand = new Random();
                        var num = rand.Next(0, 100);
                        if (num < (int)passive.Param1)
                        {
                            dmg -= (int)passive.Param2;
                        }
                        break;
                };
            }
            dmg = this.ApplyElementToDmg(srcCard, tarCard, dmg, def);
            var finalDmg = Math.Max(0, dmg);
            foreach (var passive in tarPassives)
            {
                switch (passive.Passive)
                {
                    case Passive.ENDURABLE:
                        dmg -= dmg * (int)passive.Param1 / 100;
                        break;
                };
            }
            return finalDmg;
        }

        public int CalculateExtraDmg(BattleCard srcCard, BattleCard tarCard, int dmgDone = 0)
        {
            var dmg = 0;
            var srcPassives = srcCard.PlayerCard.GetPassives();
            var tarPassives = tarCard.PlayerCard.GetPassives();
            foreach (var passive in srcPassives)
            {
                switch (passive.Passive)
                {
                    case Passive.SHATTER:
                        dmg += (int)Math.Ceiling((double)(tarCard.Hp / passive.Param1));
                        break;
                    case Passive.BLOWMARK:
                        var mark = tarCard.ActiveEffects.FirstOrDefault(x => x.Passive == Passive.BLOWMARK);
                        if (mark != null)
                        {
                            dmg += (int)mark.Param1;
                        }
                        break;
                };
            }
            return dmg;
        }

        public ICollection<BattleAction> ApplyAttackStatus(BattleCard srcCard, BattleCard tarCard, int dmgDone, ICollection<BattleCard> cards)
        {
            //TODO IF WATER ATTACK FIRE STATUS SACARLOS (BAD LUCK MEN)
            var actions = new List<BattleAction>();
            var srcPassives = srcCard.PlayerCard.GetPassives();
            var tarPassives = tarCard.PlayerCard.GetPassives();
            var random = new Random();
            var randNum = random.Next(0, 100);
            if (tarPassives.Any(x => x.Passive == Passive.IMMUNE)) return actions;
            if (tarPassives.Any(x => x.Passive == Passive.REFLECT && randNum < x.Param1))
            {
                tarCard = srcCard;
                randNum = random.Next(0, 100);
            }
            if (randNum > tarCard.GetImm())
            {
                foreach (var passive in srcPassives)
                {
                    int[] possibleTargetPos;
                    randNum = random.Next(0, 100);
                    switch (passive.Passive)
                    {
                        case Passive.BACKTRACK:
                            tarCard.Time = Math.Min(tarCard.GetSpd(), tarCard.Time + passive.Param1);
                            tarCard.Time = Math.Round(tarCard.Time, 2, MidpointRounding.AwayFromZero);
                            break;
                        case Passive.POISON:
                        case Passive.VENOM:
                        case Passive.BLIND:
                        case Passive.BLEED:
                        case Passive.FREEZE:
                        case Passive.DOOM:
                        case Passive.STUN:
                        case Passive.ELECTRIFY:
                        case Passive.CONFUSSION:
                        case Passive.DEVIATE:
                        case Passive.INTENSIFY:
                            var eff = tarCard.ActiveEffects.FirstOrDefault(x => x.Passive == passive.Passive);
                            if (eff != null)
                            {
                                tarCard.ActiveEffects.Remove(eff);
                            }
                            tarCard.ActiveEffects.Add(DoEff(srcCard.Position, passive.Id, passive.Passive, passive.Param1, passive.Param2, passive.Param3));
                            break;
                        case Passive.BLAZE:
                        case Passive.BURNOUT:
                        case Passive.INCINERATE:
                            if (!(srcCard.PlayerCard.Card.Element == Element.FIRE && tarCard.PlayerCard.Card.Element == Element.WATER))
                            {
                                var fireEff = tarCard.ActiveEffects.FirstOrDefault(x => x.Passive == passive.Passive);
                                if (fireEff != null)
                                {
                                    tarCard.ActiveEffects.Remove(fireEff);
                                }
                                tarCard.ActiveEffects.Add(DoEff(srcCard.Position, passive.Id, passive.Passive, passive.Param1, passive.Param2, passive.Param3));
                            }
                            break;
                        case Passive.BURN:
                            if (randNum < (int)passive.Param1 && !(srcCard.PlayerCard.Card.Element == Element.FIRE && tarCard.PlayerCard.Card.Element == Element.WATER))
                            {
                                actions.Add(this.ApplyDmg(tarCard, (int)passive.Param2, passive.Passive));
                                tarCard.ActiveEffects.Add(DoEff(srcCard.Position, passive.Id, passive.Passive, passive.Param1, passive.Param2, passive.Param3));
                            }
                            break;
                        case Passive.MELT:
                            if (!(srcCard.PlayerCard.Card.Element == Element.FIRE && tarCard.PlayerCard.Card.Element == Element.WATER))
                            {
                                actions.Add(this.ApplyDmg(tarCard, (int)passive.Param1, passive.Passive));
                            }
                            break;
                        case Passive.BLOWMARK:
                        case Passive.INUNDATE:
                            var mark = tarCard.ActiveEffects.FirstOrDefault(x => x.Passive == passive.Passive && x.FromPosition == srcCard.Position);
                            if (mark != null)
                            {
                                mark.Param1 += passive.Param1;
                            }
                            else
                            {
                                tarCard.ActiveEffects.Add(DoEff(srcCard.Position, passive.Id, passive.Passive, passive.Param1, passive.Param2, passive.Param3));
                            }
                            break;
                        case Passive.FRENZY:
                            var frenzy = srcCard.ActiveEffects.FirstOrDefault(x => x.Passive == passive.Passive && x.FromPosition == srcCard.Position);
                            if (frenzy != null)
                            {
                                frenzy.Param1 += passive.Param1;
                            }
                            else
                            {
                                srcCard.ActiveEffects.Add(DoEff(srcCard.Position, passive.Id, passive.Passive, passive.Param1, passive.Param2, passive.Param3));
                            }
                            break;
                        case Passive.OBLIVION:
                            if (randNum < (int)passive.Param1)
                            {
                                //TODO: Check this if can be refined
                                tarCard.Hp = 0;
                            }
                            break;
                        case Passive.KNOCKOUT:
                            if (tarCard.Hp <= passive.Param1)
                            {
                                //TODO: Check this if can be refined
                                tarCard.Hp = 0;
                            }
                            break;
                        case Passive.LIFESTEAL:
                            var healAmount = dmgDone / passive.Param1;
                            this.ApplyHeal(srcCard, (int)healAmount);
                            actions.Add(new BattleAction { Number = (int)passive.Param1, Type = HealthChange.HEAL, Position = srcCard.Position });
                            break;
                        case Passive.TRANSFUSE:
                            if (srcCard.Position < 4)
                            {
                                possibleTargetPos = cards.Where(x => x.Position > 3 && x.Hp != 0 && x.Position != srcCard.Position).Select(x => x.Position).ToArray();
                            }
                            else
                            {
                                possibleTargetPos = cards.Where(x => x.Position < 4 && x.Hp != 0 && x.Position != srcCard.Position).Select(x => x.Position).ToArray();
                            }
                            if (possibleTargetPos.Length == 0) break;
                            var healPos = possibleTargetPos[random.Next(0, possibleTargetPos.Length - 1)];
                            var transfuseAmount = dmgDone / passive.Param1;
                            this.ApplyHeal(cards.FirstOrDefault(x => x.Position == healPos), (int)transfuseAmount);
                            actions.Add(new BattleAction { Number = (int)passive.Param1, Type = HealthChange.HEAL, Position = healPos });
                            break;
                        case Passive.REGURGITATE:
                            this.ApplyHeal(srcCard, (int)passive.Param1);
                            actions.Add(new BattleAction { Number = (int)passive.Param1, Type = HealthChange.HEAL, Position = srcCard.Position });
                            break;
                        case Passive.THIEF:
                            srcCard.ActiveEffects.Add(this.DoEff(srcCard.Position, passive.Id, passive.Passive, passive.Param1));
                            break;
                        case Passive.TIMESHIFT:
                            var timeSteal = Math.Round((tarCard.GetSpd() - tarCard.Time) / passive.Param1, 2, MidpointRounding.AwayFromZero);
                            srcCard.Time = Math.Max(0, srcCard.Time - timeSteal);
                            tarCard.Time += timeSteal;
                            break;
                        case Passive.BOUNCE:
                            if (srcCard.Position < 4)
                            {
                                possibleTargetPos = cards.Where(x => x.Position > 3 && x.Hp != 0 && x.Position != tarCard.Position).Select(x => x.Position).ToArray();
                            }
                            else
                            {
                                possibleTargetPos = cards.Where(x => x.Position < 4 && x.Hp != 0 && x.Position != tarCard.Position).Select(x => x.Position).ToArray();
                            }
                            if (possibleTargetPos.Length == 0) break;
                            var targetPos = possibleTargetPos[random.Next(0, possibleTargetPos.Length - 1)];
                            actions.Add(this.ApplyDmg(cards.FirstOrDefault(x => x.Position == targetPos), (int)passive.Param1, passive.Passive));
                            break;
                        case Passive.SPIKE_ARMOR:
                            var dmg = (int)Math.Round(dmgDone / passive.Param1);
                            actions.Add(this.ApplyDmg(srcCard, dmg, Passive.SPIKE_ARMOR));
                            break;
                        case Passive.COLDING:
                            var colding = tarCard.ActiveEffects.FirstOrDefault(x => x.Passive == passive.Passive && x.FromPosition == srcCard.Position);
                            if (colding != null)
                            {
                                colding.Param1 += passive.Param1;
                            }
                            else
                            {
                                tarCard.ActiveEffects.Add(DoEff(srcCard.Position, passive.Id, passive.Passive, passive.Param1, passive.Param2, passive.Param3));
                            }
                            break;
                        case Passive.MARKDOWN:
                            var markdown = tarCard.ActiveEffects.FirstOrDefault(x => x.Passive == passive.Passive && x.FromPosition == srcCard.Position);
                            if (markdown != null)
                            {
                                markdown.Param1 += passive.Param1;
                                if (markdown.Param1 == cards.FirstOrDefault(x => x.Position == markdown.FromPosition).GetPassives().FirstOrDefault(x => x.Passive == Passive.MARKDOWN).Param1)
                                {
                                    actions.Add(this.ApplyDmg(srcCard, (int)markdown.Param2, markdown.Passive));
                                    tarCard.ActiveEffects.Remove(markdown);
                                }
                            }
                            else
                            {
                                tarCard.ActiveEffects.Add(DoEff(srcCard.Position, passive.Id, passive.Passive, passive.Param1, passive.Param2, passive.Param3));
                            }
                            break;
                        case Passive.CLEAVE:
                            BattleCard leftCard;
                            BattleCard rightCard;
                            if (srcCard.Position < 4)
                            {
                                leftCard = cards.FirstOrDefault(x => x.Position > 3 && x.Position == tarCard.Position - 1 && x.Hp > 0);
                                rightCard = cards.FirstOrDefault(x => x.Position > 3 && x.Position == tarCard.Position + 1 && x.Hp > 0);
                            }
                            else
                            {
                                leftCard = cards.FirstOrDefault(x => x.Position < 4 && x.Position == tarCard.Position - 1 && x.Hp > 0);
                                rightCard = cards.FirstOrDefault(x => x.Position < 4 && x.Position == tarCard.Position + 1 && x.Hp > 0);
                            }
                            if (leftCard != null)
                            {
                                actions.Add(this.ApplyDmg(leftCard, (int)(dmgDone * passive.Param1 / 100), passive.Passive));
                            }
                            if (rightCard != null)
                            {
                                actions.Add(this.ApplyDmg(rightCard, (int)(dmgDone * passive.Param1 / 100), passive.Passive));
                            }
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
                        case Passive.FLEE:
                            if (randNum < passive.Param1)
                            {
                                tarCard.Hp = 0;
                                actions.Add(new BattleAction { Type = HealthChange.FLEE, Position = tarCard.Position });
                            }
                            break;
                        case Passive.SCORCHED:
                            srcCard.ActiveEffects.Add(this.DoEff(srcCard.Position, passive.Id, passive.Passive, passive.Param1, passive.Param2, passive.Param3));
                            break;
                            //TODO: POISONUS Y ROUGH
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
                var eff = srcEffs.ToList()[i];
                switch (eff.Passive)
                {
                    case Passive.BLEED:
                    case Passive.BURNOUT:
                        if (eff.Param2 > 0)
                        {
                            actions.Add(this.ApplyDmg(srcCard, (int)eff.Param1, eff.Passive));
                        }
                        else
                        {
                            var ret = srcEffs.ToList();
                            ret.RemoveAt(i);
                            srcEffs = ret;
                        }
                        break;
                };
            }

            for (int i = tarEffs.Count - 1; i >= 0; i--)
            {
                var eff = tarEffs.ToList()[i];
                switch (eff.Passive)
                {
                    case Passive.BLAZE:
                    case Passive.BURNOUT:
                    case Passive.SCORCHED:
                        if (eff.Param2 > 0)
                        {
                            if (!(tarCard.PlayerCard.Card.Element == Element.FIRE && srcCard.PlayerCard.Card.Element == Element.WATER))
                            {
                                actions.Add(this.ApplyDmg(tarCard, (int)eff.Param1, eff.Passive));
                            }
                        }
                        else
                        {
                            var ret = srcEffs.ToList();
                            ret.RemoveAt(i);
                            srcEffs = ret;
                        }
                        break;
                    case Passive.ABLAZE:
                        if (!(tarCard.PlayerCard.Card.Element == Element.FIRE && srcCard.PlayerCard.Card.Element == Element.WATER))
                        {
                            actions.Add(this.ApplyDmg(srcCard, (int)eff.Param1, eff.Passive));
                        }
                        break;
                };
            }
            return actions;
        }

        public ICollection<BattleAction> TriggerDeathPassive(BattleCard srcCard, ICollection<BattleCard> cards, int killerPosition)
        {
            var srcPassives = srcCard.PlayerCard.GetPassives();
            var actions = new List<BattleAction>();
            var killer = cards.FirstOrDefault(x => x.Position == killerPosition);
            foreach (var passive in srcPassives)
            {
                switch (passive.Passive)
                {
                    case Passive.ENERGYBOMB:
                        actions.Add(this.ApplyDmg(killer, (int)passive.Param1, Passive.ENERGYBOMB));
                        break;
                    case Passive.REAPER:
                        BattleCard reaperTarget = null;
                        if (srcCard.Position < 4)
                        {
                            var enemyCount = cards.Count(x => x.Position > 3 && x.Hp > 0);
                            if (enemyCount > 1)
                            {
                                reaperTarget = cards.FirstOrDefault(x => x.Position > 3 && x.Hp == cards.Where(x => x.Position > 3).Max(x => x.Hp));
                            }
                        }
                        else
                        {
                            var enemyCount = cards.Count(x => x.Position < 4 && x.Hp > 0);
                            if (enemyCount > 1)
                            {
                                reaperTarget = cards.FirstOrDefault(x => x.Position < 4 && x.Hp == cards.Where(x => x.Position < 4).Max(x => x.Hp));
                            }
                        }
                        if (reaperTarget != null)
                        {
                            reaperTarget.Hp = 0;
                            actions.Add(new BattleAction { Position = reaperTarget.Position, Type = HealthChange.DEATH });
                        }

                        break;
                };
            }
            return actions;
        }

        public int ApplyElementToDmg(BattleCard srcCard, BattleCard tarCard, int dmg, int def)
        {
            var src = srcCard.PlayerCard.Card.Element;
            var tar = tarCard.PlayerCard.Card.Element;
            //FIRE --> NATURE extra dmg 50%
            //WATER--> FIRE no puede ser quemado water
            //EARTH --> WATER backtrack 1 seg
            //WIND --> EARTH si el danio es 0, al menos sera 1
            //DARK --> WIND un 10% de matarlo de una
            //LIGHT --> DARK se cura TIER
            //NATURE --> LIGHT un 25% de doble damage
            var random = new Random();
            var num = random.Next(0, 100);
            var finalDmg = Math.Max(dmg - def, 0);
            if (src == Element.FIRE && tar == Element.NATURE && num < 50)
            {
                finalDmg = Math.Max(0, (int)(dmg * 1.5) - def);
            }
            if (src == Element.EARTH && tar == Element.WATER)
            {
                tarCard.Time = Math.Min(tarCard.GetSpd(), tarCard.Time + 1);
            }
            if (src == Element.WIND && tar == Element.EARTH)
            {
                finalDmg = Math.Max(0, dmg - def);
                if (finalDmg == 0)
                {
                    finalDmg = Math.Min(1, dmg + srcCard.PlayerCard.Card.Tier - def);
                }
            }
            if (src == Element.DARK && tar == Element.WIND && num < 10)
            {
                finalDmg = 999;
            }
            if (src == Element.LIGHT && tar == Element.DARK)
            {
                srcCard.Hp = Math.Min(srcCard.GetHp(), srcCard.Hp + srcCard.PlayerCard.Card.Tier);
            }
            if (src == Element.NATURE && tar == Element.LIGHT && num < 25)
            {
                finalDmg = Math.Max(0, dmg - def) * 2;
            }
            return finalDmg;
        }

        public BattleAction ApplyDmg(BattleCard card, int dmg, Passive passive = Passive.NONE)
        {
            var intensify = card.ActiveEffects.FirstOrDefault(x => x.Passive == Passive.INTENSIFY);
            if (intensify != null && passive != Passive.NONE)
            {
                dmg += (int)Math.Round(dmg * intensify.Param1 / 100);
            }
            if (card.GetPassives().Any(x => x.Passive == Passive.FLAMMABLE))
            {
                dmg = dmg * 2;
            }
            if (passive == Passive.NONE && card.Shield > 0)
            {
                card.Shield -= dmg;
                if (card.Shield < 0)
                {
                    card.Hp += card.Shield;
                    card.Shield = 0;
                }
            }
            else
            {
                card.Hp -= dmg;
            }
            if (card.Hp < 0) card.Hp = 0;
            var action = new BattleAction
            {
                Number = dmg,
                Position = card.Position
            };
            action.Type = this.GetActionType(passive);

            return action;
        }

        public void ApplyHeal(BattleCard card, int healAmount)
        {
            card.Hp += healAmount;
            if (card.Hp > card.GetHp())
            {
                card.Hp = card.GetHp();
            }
        }

        public ActiveEffect DoEff(int fromPosition, int id, Passive p, double p1, double p2 = 0, double p3 = 0)
        {
            return new ActiveEffect
            {
                FromPosition = fromPosition,
                Id = id,
                Passive = p,
                Param1 = p1,
                Param2 = p2,
                Param3 = p3,
            };
        }

        public HealthChange GetActionType(Passive p) =>
        p switch
        {
            Passive.BLAZE => HealthChange.BLAZE,
            Passive.ABLAZE => HealthChange.BLAZE,
            Passive.BURN => HealthChange.BLAZE,
            Passive.BURNOUT => HealthChange.BLAZE,
            Passive.SCORCHED => HealthChange.BLAZE,
            Passive.MELT => HealthChange.BLAZE,
            Passive.HELLFIRE => HealthChange.BLAZE,
            Passive.INCINERATE => HealthChange.BLAZE,
            Passive.POISON => HealthChange.POISON,
            Passive.POISONUS => HealthChange.POISON,
            Passive.VENOM => HealthChange.POISON,
            Passive.ENERGYBOMB => HealthChange.DAMAGE, //CHECK IF NEED CHANGE
            Passive.BLEED => HealthChange.BLEED,
            _ => HealthChange.DAMAGE,
        };
    }
}
