using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class MapRequiredEnemy : IEntity
    {
        public int MapId { get; set; }
        public int EnemyId { get; set; }
        public virtual Map Map { get; set; }
        public virtual EnemyNpc Enemy { get; set; }
    }
}
