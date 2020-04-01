using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public abstract class Recipe : IEntity
    {
        public List<RequiredItem> RequiredItems { get; set; }
    }

    public class CardRecipe : Recipe
    {
        public Card Result { get; set; }
    }

    public class RuneRecipe : Recipe
    {
        public Rune Result { get; set; }
    }

    public class ItemRecipe : Recipe
    {
        public Item Result { get; set; }
    }
}
