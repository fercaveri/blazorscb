using System;

namespace SurrealCB.Data.Model
{
    public class CardMetric : IEntity
    {
        public int CardId { get; set; }
        public int Level { get; set; }
        public bool Died { get; set; }
        public bool Win { get; set; }
    }
}
