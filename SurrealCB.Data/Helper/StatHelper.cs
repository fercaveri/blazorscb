using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SurrealCB.Data.Model;

namespace SurrealCB.Data.Helper
{
    public class StatHelper
    {
        public static double BoostStat(double amount, BoostType type, ICollection<LevelBoost> levelBoosts, ICollection<Rune> runes)
        {
            var flatBoosts = new List<StatBoost>();
            var percBoosts = new List<StatBoost>();
            var percType = StatHelper.GetPercType(type);
            foreach (var statBoost in levelBoosts.Select(x => x.Boost.StatBoosts))
            {
                foreach (var filteredBoost in statBoost.Where(x => x.Type == percType))
                {
                    percBoosts.Add(filteredBoost);
                }
                foreach (var filteredBoost in statBoost.Where(x => x.Type == type))
                {
                    flatBoosts.Add(filteredBoost);
                }
            }
            foreach (var rune in runes.Where(x => x != null).Select(x => x.Boost.StatBoosts))
            {
                foreach (var filteredBoost in rune.Where(x => x.Type == percType))
                {
                    percBoosts.Add(filteredBoost);
                }
                foreach (var filteredBoost in rune.Where(x => x.Type == type))
                {
                    flatBoosts.Add(filteredBoost);
                }
            }
            var baseAmount = amount;
            foreach (var boost in percBoosts)
            {
                if (type == BoostType.SPD)
                {
                    amount -= (baseAmount * boost.Amount / 100);
                    amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    amount += (baseAmount * boost.Amount / 100);
                }
            }

            foreach (var boost in flatBoosts)
            {
                if (type == BoostType.SPD)
                {
                    amount -= boost.Amount;
                    amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    amount += boost.Amount;
                }
            }
            return amount;
        }

        private static BoostType GetPercType(BoostType type) =>
            type switch
            {
                BoostType.HP => BoostType.HPPERC,
                BoostType.ATK => BoostType.ATKPERC,
                BoostType.DEF => BoostType.DEFPERC,
                BoostType.SPD => BoostType.SPDPERC,
                BoostType.IMM => BoostType.IMMPERC,
                _ => BoostType.HPPERC
            };
    }
}
