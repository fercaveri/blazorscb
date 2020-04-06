using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class BattleAction
    {
        public int Position { get; set; }
        public int Number { get; set; }
        public HealthChange Type { get; set; }
    }
}
