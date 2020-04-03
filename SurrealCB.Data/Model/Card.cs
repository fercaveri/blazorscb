using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurrealCB.Data.Model
{
    public class Card : Stats
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; } 
        public string Name { get; set; }
        public int Tier { get; set; }
        public Rarity Rarity { get; set; }
        public AtkType AtkType { get; set; }
        public Element Element { get; set; }
        public virtual CardPassive Passive { get; set; }
        public virtual List<CardBoost> LevelBoosts { get; set; }
        public int Value { get; set; }
        public int BaseExp { get; set; }
        public int RuneSlots { get; set; }
        public string ImgSrc { get; set; }
    }
}
