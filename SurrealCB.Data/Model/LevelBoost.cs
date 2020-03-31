using System;
using System.Collections.Generic;
using System.Text;

namespace SurrealCB.Data.Model
{
    public class LevelBoost : IEntity
    {
        public int Level { get; set; }
        public CardBoost Boost { get; set; }
        public string ImprovedName { get; set; }
        public List<RequiredItem> RequiredItems { get; set; }
        public int Cost { get; set; }
    }
}
