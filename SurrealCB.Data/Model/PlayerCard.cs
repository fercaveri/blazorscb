using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SurrealCB.Data.Helper;

namespace SurrealCB.Data.Model
{
    public class PlayerCard : IEntity
    {
        public int CurrentExp { get; set; }
        public virtual List<Rune> Runes { get; set; } = new List<Rune>();
        public virtual List<LevelBoost> ActiveLvlBoosts { get; set; } = new List<LevelBoost>();
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
            if (this.ActiveLvlBoosts.Any())
            {
                //TODO: ESTOY AGARRANDO LA PRIMER PASIVA NOMAS
                var secondList = this.ActiveLvlBoosts.Where(x => x.Boost.Passives?.Any() == true);
                if (secondList.Any())
                {
                    list = list.Concat(secondList.OrderBy(x => x.Level).Select(x => x.Boost.Passives).FirstOrDefault()).ToList();
                }

            }
            list = list.GroupBy(p => p.Passive).Select(g => g.Last()).ToList();
            return list;
        }

        public int GetHp()
        {
            return (int)StatHelper.BoostStat(this.Card.Hp, BoostType.HP, this.ActiveLvlBoosts, this.GetRuneList());
        }

        public int GetAtk()
        {
            return (int)StatHelper.BoostStat(this.Card.Atk, BoostType.ATK, this.ActiveLvlBoosts, this.GetRuneList());
        }

        public int GetDef()
        {
            return (int)StatHelper.BoostStat(this.Card.Def, BoostType.DEF, this.ActiveLvlBoosts, this.GetRuneList());
        }

        public int GetImm()
        {
            return (int)StatHelper.BoostStat(this.Card.Imm, BoostType.IMM, this.ActiveLvlBoosts, this.GetRuneList());
        }

        public double GetSpd()
        {
            return StatHelper.BoostStat(this.Card.Spd, BoostType.SPD, this.ActiveLvlBoosts, this.GetRuneList());
        }

        public string GetName()
        {
            var improvedName = this.ActiveLvlBoosts.OrderByDescending(x => x.Level).FirstOrDefault(x => !string.IsNullOrEmpty(x.ImprovedName))?.ImprovedName;
            return !string.IsNullOrEmpty(improvedName) ? improvedName : this.Card.Name;
        }

        public int GetLevel()
        {
            if (this.ActiveLvlBoosts?.Any() != true) return 1;
            return this.ActiveLvlBoosts.Max(x => x.Level);
        }

        public List<Rune> GetRuneList()
        {
            var runes = this.Runes;
            var diff = this.Card.RuneSlots - runes.Count;
            for (var i = 0; i < diff; i++)
            {
                runes.Add(null);
            }
            return runes;
        }
    }
}
