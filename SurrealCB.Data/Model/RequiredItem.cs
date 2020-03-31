using System;

namespace SurrealCB.Data.Model
{
    public class RequiredItem : IEntity
    {
        public Item Obj { get; set; }
        public int Amount { get; set; }
    }

}
