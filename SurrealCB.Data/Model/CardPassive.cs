using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurrealCB.Data.Model
{
    public class CardPassive : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }
        public Passive Passive { get; set; }
        public double Param1 { get; set; }
        public double Param2 { get; set; }
        public double Param3 { get; set; }
    }
}
