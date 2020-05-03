using System;
using System.Collections.Generic;
using SurrealCB.Data.Enum;

namespace SurrealCB.Data.Model
{
    public class PlayerRune : IEntity
    {
        public Rarity Rarity { get; set; }
        public virtual Rune Rune { get; set; }
    }
}
