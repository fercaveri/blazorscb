using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class CardBoost : IEntity
    {
        public virtual ICollection<StatBoost> StatBoosts { get; set; }
        public virtual ICollection<CardPassive> Passives { get; set; }
    }
}
