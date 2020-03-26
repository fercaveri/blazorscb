using System;

namespace SurrealCB.Data.Model
{
    public class PlayerCard : Card
    {
        public string Owner { get; set; }
        public int CurrentExp { get; set; }
        public CardBoost[] LevelBoosts { get; set; }
        public Rune[] Runes { get; set; }
    }
}
