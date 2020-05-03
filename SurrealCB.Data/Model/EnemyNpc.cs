using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurrealCB.Data.Model
{
    public class EnemyNpc : IEntity
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public virtual ICollection<PlayerCard> Cards { get; set; }
        public int CardCount { get; set; }
        public bool RandomCards { get; set; }
        public virtual Reward Reward { get; set; }
        //public virtual ICollection<MapRequiredEnemy> RequiredToMaps { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
