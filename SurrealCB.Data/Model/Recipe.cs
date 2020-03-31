using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class Recipe : IEntity
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public List<RequiredItem> RequiredItems { get; set; }
    }
}
