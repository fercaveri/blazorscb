using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class CardBoost : Stats
    {
        public CardPassive Passive { get; set; }
        //Si quiero mejorar una pasiva existente usar un nuevo cardpassive con el mismo id
    }
}
