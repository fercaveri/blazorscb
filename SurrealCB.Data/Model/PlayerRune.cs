using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class PlayerRune : IEntity
    {
        public int RuneId { get; set; }
        public Rarity Rarity { get; set; }
        public virtual Rune Rune { get; set; }
    }
}
