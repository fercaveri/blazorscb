using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
Public abstract class Stats : IEntity {
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Imm { get; set; } //Immunity
        public double Spd { get; set; }
}

    public class Card : Stats
    {
        public string Name { get; set; }
        public int Tier { get; set; }
        public Rarity Rarity { get; set; }
        public AtkType AtkType { get; set; }
        public Element Element { get; set; }
        public CardPassive Passive { get; set; }
        public List<CardBoost> LevelBoosts { get; set; }
        public int Value { get; set; }
        public int BaseExp { get; set; }
        public int RuneSlots { get; set; }
        public string ImgSrc { get; set; }
    }
}
