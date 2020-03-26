using System;

namespace SurrealCB.Data.Model
{
    public class CardPassive : IEntity
    {
        public Passive Passive { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
    }
}
