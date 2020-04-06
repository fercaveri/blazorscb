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
        Task<int> NextTurn(ICollection<BattleCard> cards);
        Task<BattleEnd> CheckWinOrLose(ICollection<BattleCard> cards, int srcPos);
    }
    public class BattleService : IBattleService
    {
        private readonly SCBDbContext repository;

        public BattleService(SCBDbContext repository)
        {
            this.repository = repository;
        }

        public Task<int> NextTurn(ICollection<BattleCard> cards)
        {
            //var globalEffects = List<EffectOAlgo>();
            //TODO: ver efectos de tiempo tipo freeze etc
            var nextCard = cards.Where(x => x.Hp != 0).OrderBy(x => x.Time).FirstOrDefault();
            var timeElapsed = nextCard.Time;
            foreach (var card in cards)
            {
                card.Time = Math.Max(card.Time - timeElapsed, 0);
            }
            nextCard.Time = nextCard.GetSpd();
            return Task.FromResult(nextCard.Position);
        }

        public Task<ICollection<BattleAction>> PerformAttack(BattleCard srcCard, BattleCard tarCard)
        {
            ICollection<BattleAction> retList = new List<BattleAction>();
            var cancelStatus = this.ShouldCancelAttack(srcCard, tarCard);
            if  (cancelStatus != CancelStatus.NONE)
            {
                return Task.FromResult(retList);
            }
            var dmg = this.CalculateDmg(srcCard, tarCard);
            var extraDmg = this.CalculateExtraDmg(srcCard, tarCard);
            tarCard.Hp = tarCard.Hp - dmg - extraDmg;
            if (tarCard.Hp < 0) tarCard.Hp = 0; 

            var dmgType = tarCard.Hp == 0 ? HealthChange.DEATH : HealthChange.DAMAGE;
            retList.Add(new BattleAction { Number = dmg + extraDmg, Position = tarCard.Position, Type = dmgType });

            return Task.FromResult(retList);
        }

        public Task<ICollection<BattleAction>> AttackAll(BattleCard srcCard, ICollection<BattleCard> targets)
        {
            ICollection<BattleAction> retList = new List<BattleAction>();

            foreach(var tarCard in targets)
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

        public int ApplyElementToDmg(Element src, Element tar, int dmg)
        {
            //TODO: hacerlo
            return dmg;
        }
    }
}
