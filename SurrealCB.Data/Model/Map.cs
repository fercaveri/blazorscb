using System;
using System.Collections.Generic;
using SurrealCB.Data.Enum;

namespace SurrealCB.Data.Model
{
    public class Map : IEntity
    {
        public string Name { get; set; }
        public int MinLevel { get; set; }
        public MapDifficult Difficult { get; set; }
        public GameType GameType { get; set; }
        public virtual ICollection<EnemyNpc> Enemies { get; set; }
        public virtual Reward CompletionReward { get; set; }
        //public virtual List<Map> RequiredMaps { get; set; }
        public virtual ICollection<EnemyNpc> RequiredEnemies { get; set; }
        public string SrcImg { get; set; }
    }
}
