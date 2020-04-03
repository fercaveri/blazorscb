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
        Task PerformAttack(BattleCard srcCard, BattleCard tarCard);
    }
    public class BattleService : IBattleService
    {
        private readonly SCBDbContext repository;

        public BattleService(SCBDbContext repository)
        {
            this.repository = repository;
        }

        public Task PerformAttack(BattleCard srcCard, BattleCard tarCard)
        {
            var cancelStatus = this.ShouldCancelAttack(srcCard, tarCard);
            if  (cancelStatus != CancelStatus.NONE)
            {
                return Task.CompletedTask;
            }
            var dmg = this.CalculateDmg(srcCard, tarCard);
            tarCard.Hp = Math.Max(this.ApplyDmg(srcCard, tarCard, dmg), 0);
            var extraDmg = this.CalculateExtraDmg(srcCard, tarCard);
            tarCard.Hp = Math.Max(tarCard.Hp - extraDmg, 0);
            return Task.CompletedTask;
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
            return dmg - def;
        }

        public int ApplyDmg(BattleCard srcCard, BattleCard tarCard, int dmg)
        {
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
            var finalDmg = dmg - def;
            finalDmg = this.ApplyElementToDmg(srcCard.PlayerCard.Card.Element, tarCard.PlayerCard.Card.Element, finalDmg);
            return finalDmg;
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
