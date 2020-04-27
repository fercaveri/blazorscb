using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SurrealCB.Data.Model
{
    public class PlayerCard : IEntity
    {
        public int CurrentExp { get; set; }
        public virtual ICollection<Rune> Runes { get; set; }
        public virtual ICollection<ActiveLevelBoost> ActiveLvlBoosts { get; set; }
        public int CardId { get; set; }
        public virtual Card Card { get; set; }
        public int? EnemyNpcId { get; set; }
        public Guid? ApplicationUserId { get; set; }
        public virtual List<CardPassive> GetPassives()
        {
            //TODO: FIX NULL PASSIVE
            var list = new List<CardPassive> { };
            if (this.Card.Passive != null)
            {
                list.Add(this.Card.Passive);
            }
            var levelOnePassives = this.Card.LevelBoosts.Where(x => x.Level == 1);
            if (levelOnePassives?.Any() == true)
            {
                foreach (var lb in levelOnePassives)
                {
                    var passive = lb.Boost.Passives.FirstOrDefault();
                    if (passive != null)
                    {
                        list.Add(passive);
                    }
                }
            }
            if (this.ActiveLvlBoosts.Any())
            {
                //TODO: ESTOY AGARRANDO LA PRIMER PASIVA NOMAS
                var secondList = this.ActiveLvlBoosts.Where(x => x.LevelBoost?.Boost?.Passives?.Any() == true);
                if (secondList.Any())
                {
                    list = list.Concat(secondList.OrderBy(x => x.LevelBoost?.Level).Select(x => x.LevelBoost.Boost.Passives).FirstOrDefault()).ToList();
                }

            }
            list = list.GroupBy(p => p.Passive).Select(g => g.Last()).ToList();
            return list;
        }

        public int GetHp()
        {
            return (int)this.BoostStat(this.Card.Hp, BoostType.HP);
        }

        public int GetAtk()
        {
            return (int)this.BoostStat(this.Card.Atk, BoostType.ATK);
        }

        public int GetDef()
        {
            return (int)this.BoostStat(this.Card.Def, BoostType.DEF);
        }

        public int GetImm()
        {
            return (int)this.BoostStat(this.Card.Imm, BoostType.IMM);
        }

        public double GetSpd()
        {
            return this.BoostStat(this.Card.Spd, BoostType.SPD);
        }

        public string GetName()
        {
            var improvedName = this.ActiveLvlBoosts.OrderByDescending(x => x.LevelBoost.Level).FirstOrDefault(x => !string.IsNullOrEmpty(x.LevelBoost.ImprovedName))?.LevelBoost.ImprovedName;
            return !string.IsNullOrEmpty(improvedName) ? improvedName : this.Card.Name;
        }

        public int GetLevel()
        {
            if (this.ActiveLvlBoosts?.Any() != true) return 1;
            return this.ActiveLvlBoosts.Max(x => x.LevelBoost.Level);
        }

        public List<Rune> GetRuneList()
        {
            var runes = this.Runes;
            var diff = this.Card.RuneSlots - runes.Count;
            for (var i = 0; i < diff; i++)
            {
                runes.Add(null);
            }
            return runes.ToList();
        }

        public double BoostStat(double amount, BoostType type)
        {
            var flatBoosts = new List<StatBoost>();
            var percBoosts = new List<StatBoost>();
            var percType = this.GetPercType(type);
            foreach (var statBoost in this.ActiveLvlBoosts?.Where(x => x != null && x.LevelBoost.Boost != null && x.LevelBoost.Boost.StatBoosts != null).Select(x => x.LevelBoost.Boost.StatBoosts).Where(x => x != null))
            {
                foreach (var filteredBoost in statBoost.Where(x => x?.Type == percType))
                {
                    percBoosts.Add(filteredBoost);
                }
                foreach (var filteredBoost in statBoost.Where(x => x?.Type == type))
                {
                    flatBoosts.Add(filteredBoost);
                }
            }
            foreach (var rune in this.GetRuneList()?.Where(x => x != null && x.Boost != null && x.Boost.StatBoosts != null).Select(x => x.Boost.StatBoosts).Where(x => x != null))
            {
                foreach (var filteredBoost in rune.Where(x => x?.Type == percType))
                {
                    percBoosts.Add(filteredBoost);
                }
                foreach (var filteredBoost in rune.Where(x => x?.Type == type))
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

        private BoostType GetPercType(BoostType type) =>
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
