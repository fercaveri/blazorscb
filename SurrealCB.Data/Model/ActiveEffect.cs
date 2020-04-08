using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurrealCB.Data.Model
{
    public class ActiveEffect : CardPassive
    {
        public int FromPosition { get; set; }

        //public ActiveEffect(CardPassive passive, int pos)
        //{
        //    this.Id = passive.Id;
        //    this.Param1 = passive.Param1;
        //    this.Param2 = passive.Param2;
        //    this.Param3 = passive.Param3;
        //    this.Passive = passive.Passive;
        //    this.FromPosition = pos;
        //}
    }
}
