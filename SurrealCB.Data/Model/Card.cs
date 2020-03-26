using System;

namespace SurrealCB.Data.Model
{
    public class Card : IEntity
    {
        public string Name { get; set; }
        public int Tier { get; set; }
        public Rarity Rarity { get; set; }
        public AtkType AtkType { get; set; }
        public Element Element { get; set; }
        public CardPassive Passive { get; set; } 
        public int Vit { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public long Spd { get; set; }
        public int Value { get; set; }
        public int BaseExp { get; set; }
        public int SlotCount { get; set; }
        public string ImgSrc { get; set; }
    }
}
