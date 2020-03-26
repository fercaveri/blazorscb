using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class PlayerCard : Card
    {
        public string Owner { get; set; }
        public int CurrentExp { get; set; }
        public List<Rune> Runes { get; set; }
    }
}
