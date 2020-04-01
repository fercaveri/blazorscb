using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class Map : IEntity
    {
        public string Name { get; set; }
        public int MinLevel { get; set; }
        public GameType Type { get; set; }
        public List<EnemyNpc> Enemies { get; set; }
        public Reward CompletionReward { get; set; }
        public List<Map> RequiredMaps { get; set; }
        public List<EnemyNpc> RequiredEnemies { get; set; }
    }
}
