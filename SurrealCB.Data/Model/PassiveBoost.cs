using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class PassiveBoost : IEntity
    {
        public virtual CardPassive CardPassive { get; set; }
    }
}
