using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class MapRequiredEnemy : IEntity
    {
        public int MapId { get; set; }
        public int EnemyId { get; set; }
        public Map Map { get; set; }
        public EnemyNpc Enemy { get; set; }
    }
}
