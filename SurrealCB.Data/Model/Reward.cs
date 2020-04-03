using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class Reward : IEntity
    {
        public int Gold { get; set; }
        public int Exp { get; set; }
        public virtual List<Item> Items { get; set; }
        public virtual Card Card { get; set; }
    }
}
