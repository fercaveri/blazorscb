using System;

namespace SurrealCB.Data.Model
{
    public class CardBoost : IEntity
    {
        public int Vit { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public long Spd { get; set; }
        public CardPassive Passive { get; set; }
    }
}
