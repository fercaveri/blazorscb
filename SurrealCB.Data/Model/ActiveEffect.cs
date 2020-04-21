using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurrealCB.Data.Model
{
    public class ActiveEffect : CardPassive
    {
        public int FromPosition { get; set; }
    }
}
