using System;
using System.ComponentModel.DataAnnotations.Schema;
using SurrealCB.Data.Enum;

namespace SurrealCB.Data.Model
{
    public class CardPassive : IEntity
    {
        public Passive Passive { get; set; }
        public double Param1 { get; set; }
        public double Param2 { get; set; }
        public double Param3 { get; set; }
    }
}
