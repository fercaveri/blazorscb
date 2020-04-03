using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public abstract class Recipe : IEntity
    {
        virtual public List<RequiredItem> RequiredItems { get; set; }
    }

    public class CardRecipe : Recipe
    {
        public virtual Card Result { get; set; }
    }

    public class RuneRecipe : Recipe
    {
        public virtual Rune Result { get; set; }
    }

    public class ItemRecipe : Recipe
    {
        public virtual Item Result { get; set; }
    }
}
