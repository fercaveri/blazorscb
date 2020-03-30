using System;

namespace SurrealCB.Data.Model
{
    public class CardBoost : IEntity
    {
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public double Spd { get; set; }
        public CardPassive Passive { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
    }
}
