using System;

namespace SurrealCB.Data.Model
{
    public class RequiredItem : IEntity
    {
        public virtual Item Item { get; set; }
        public int Amount { get; set; }
    }

}
