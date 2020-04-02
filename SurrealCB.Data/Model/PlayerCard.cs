using System;
using System.Collections.Generic;
using System.Linq;

namespace SurrealCB.Data.Model
{
    public class PlayerCard : IEntity
    {
        public string Owner { get; set; }
        public int CurrentExp { get; set; }
        public List<Rune> Runes { get; set; }
        public List<LevelBoost> ActiveLvlBoosts { get; set; }
        public Card Card { get; set; }
        public List<CardPassive> GetPassives()
        {
            var list = new List<CardPassive> { this.Card.Passive };
            list = list.Concat(this.ActiveLvlBoosts.OrderBy(x => x.Level).Select(x => x.Boost.Passive)).Distinct().ToList();
            return list;
        }

        public int GetHp()
        {
            var hp = this.Card.Hp;
            //TODO: Rune & Boost
            return hp;
        }

        public int GetAtk()
        {
            var attack = this.Card.Atk;
            //TODO: Rune & Boost
            return attack;
        }

        public int GetDef()
        {
            var deffense = this.Card.Def;
            //TODO: Rune & Boost
            return deffense;
        }

        public double GetImm()
        {
            var immunity = this.Card.Imm;
            //TODO: Rune & Boost
            return immunity;
        }

        public double GetSpd()
        {
            var speed = this.Card.Spd;
            //TODO: Rune & Boost
            return speed;
        }
    }
}
