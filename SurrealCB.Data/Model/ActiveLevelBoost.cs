using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class ActiveLevelBoost : IEntity
    {
        public int LevelBoostId { get; set; }
        public int PlayerCardId { get; set; }
        public virtual LevelBoost LevelBoost { get; set; }
    }
}
