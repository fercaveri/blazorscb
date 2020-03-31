using System;

namespace SurrealCB.Data.Model
{
    public class Map : IEntity
    {
        public string Name { get; set; }
        public int MinLevel { get; set; }
        public List<EnemyNpc> Enemies { get; set; }
        public List<Reward> CompletionRewards { get; set; }
        //Puede ser GoldReward, ItemReward, ??
        public List<Map> RequiredMaps { get; set; }
        public List<EnemyNpc> RequiredEnemies { get; set; }
    }
}
