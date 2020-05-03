using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class PassiveBoost : IEntity
    {
        public int CardPassiveId { get; set; }
        public virtual CardPassive CardPassive { get; set; }
    }
}
