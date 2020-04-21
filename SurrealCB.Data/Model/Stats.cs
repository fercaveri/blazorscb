using System;
using System.Collections.Generic;
using System.Text;

namespace SurrealCB.Data.Model
{
    public abstract class Stats : IEntity
    {
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Imm { get; set; }
        public double Spd { get; set; }
    }
}
