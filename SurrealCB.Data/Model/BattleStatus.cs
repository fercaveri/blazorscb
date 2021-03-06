using System;
using System.Collections.Generic;
using SurrealCB.Data.Enum;

namespace SurrealCB.Data.Model
{
    public class BattleStatus : IEntity
    {
        public int NextPosition { get; set; }
        public virtual ICollection<BattleCard> Cards { get; set; }
        public virtual ICollection<BattleAction> Actions { get; set; }
        public BattleEnd Status { get; set; }
    }
}
