using System;

namespace SurrealCB.Data.Model
{
    public class CardPassive : IEntity
    {
        public Passive Passive { get; set; }
        public long Param1 { get; set; }
        public long Param2 { get; set; }
        public long Param3 { get; set; }
    }
}
