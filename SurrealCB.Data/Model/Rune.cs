using System;
using System.Collections.Generic;
using SurrealCB.Data.Enum;

namespace SurrealCB.Data.Model
{
    public class Rune : IEntity
    {
        public string Name { get; set; }
        public virtual CardBoost Boost { get; set; }
        public Rarity Rarity { get; set; }
        public Element Element { get; set; }
        public int Value { get; set; }
        public int MinTier { get; set; }
        public int MaxTier { get; set; }
        public string ImgSrc { get; set; }
    }
}
