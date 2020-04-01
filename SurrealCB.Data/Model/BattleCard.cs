using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class BattleCard : IEntity
    {
        public int Position { get; set; }
        public List<CardPassive> ActiveEffects { get; set; }
        public PlayerCard Card { get; set; }
    }
}
