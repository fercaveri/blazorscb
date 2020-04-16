using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SurrealCB.Data.Model
{
    public class PlayerCard : IEntity
    {
        public int CurrentExp { get; set; }
        public virtual List<Rune> Runes { get; set; }
        public virtual List<LevelBoost> ActiveLvlBoosts { get; set; }
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
            list = list.Concat(this.ActiveLvlBoosts.Where(x => x.Boost.Passive != null).OrderBy(x => x.Level).Select(x => x.Boost.Passive)).ToList();
            list = list.GroupBy(p => p.Passive).Select(g => g.Last()).ToList();
            return list;
        }

        public int GetHp()
        {
            var hp = this.Card.Hp;
            hp += this.ActiveLvlBoosts.Where(x => x.Boost.Hp > 0).Sum(x => x.Boost.Hp);
            //TODO: RUNES;
            return hp;
        }

        public int GetAtk()
        {
            var attack = this.Card.Atk;
            attack += this.ActiveLvlBoosts.Where(x => x.Boost.Atk > 0).Sum(x => x.Boost.Atk);
            //TODO: RUNES;
            return attack;
        }

        public int GetDef()
        {
            var deffense = this.Card.Def;
            deffense += this.ActiveLvlBoosts.Where(x => x.Boost.Def > 0).Sum(x => x.Boost.Def);
            return deffense;
        }

        public int GetImm()
        {
            var immunity = this.Card.Imm;
            immunity += this.ActiveLvlBoosts.Where(x => x.Boost.Imm > 0).Sum(x => x.Boost.Imm);
            //TODO: Rune 
            return immunity;
        }

        public double GetSpd()
        {
            var speed = this.Card.Spd;
            speed -= this.ActiveLvlBoosts.Where(x => x.Boost.Spd > 0).Sum(x => x.Boost.Spd);
            //TODO: Rune 
            speed = Math.Round(speed, 2, MidpointRounding.AwayFromZero);
            return speed;
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
    }
}
