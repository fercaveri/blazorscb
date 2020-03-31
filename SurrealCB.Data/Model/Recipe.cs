using System;

namespace SurrealCB.Data.Model
{
    public class Recipe : IEntity
    {
        public string Name { get; set; }
        public int Value { get; set; }
//Public List<RequiredObject> RequiredObjects {get; set;}
    }
}
