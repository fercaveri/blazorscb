using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class PlayerCard : IEntity
    {
        public string Owner { get; set; }
        public int CurrentExp { get; set; }
        public List<Rune> Runes { get; set; }
        public List<LevelBoost> ActiveLvlBoosts { get; set; }
        public Card Card { get; set; }
    }
}
