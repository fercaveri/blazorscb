using System;

namespace SurrealCB.Data.Model
{
    public class EnemyNpc : IEntity
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int ExpGain { get; set; }
        public List<PlayerCard> Cards { get; set; }
        public List<Reward> Rewards { get; set; }
        //Puede ser GoldReward, ItemReward, ??
    }
}
