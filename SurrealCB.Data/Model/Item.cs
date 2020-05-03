using System;
using SurrealCB.Data.Enum;

namespace SurrealCB.Data.Model
{
    public class Item : IEntity
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public ItemType ItemType { get; set; }
        public int Tier { get; set; }
        public Rarity Rarity { get; set; }
    }
}
