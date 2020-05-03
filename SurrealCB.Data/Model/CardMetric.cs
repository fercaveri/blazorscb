using System;

namespace SurrealCB.Data.Model
{
    public class CardMetric : IEntity
    {
        public Card Card { get; set; }
        public int Level { get; set; }
        public bool Died { get; set; }
        public bool Win { get; set; }
    }
}
