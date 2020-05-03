using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class ActiveLevelBoost : IEntity
    {
        public virtual LevelBoost LevelBoost { get; set; }
        //public virtual PlayerCard PlayerCard { get; set; }
    }
}
