using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class EnemyNpc : IEntity
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int ExpGain { get; set; }
        public List<PlayerCard> Cards { get; set; }
        public Reward Reward { get; set; }
    }
}
