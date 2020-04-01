using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class Reward : IEntity
    {
        public int Gold { get; set; }
        public int Exp { get; set; }
        public List<Item> Items { get; set; }
        public Card Card { get; set; }
    }
}
