using System;
using System.Collections.Generic;
using SurrealCB.Data.Enum;

namespace SurrealCB.Data.Model
{
    public class BattleAction : IEntity
    {
        public int Position { get; set; }
        public int Number { get; set; }
        public HealthChange Type { get; set; }
    }
}
