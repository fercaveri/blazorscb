using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class StatBoost : IEntity
    {
        public BoostType Type { get; set; }
        public double Amount { get; set; }
    }
}
