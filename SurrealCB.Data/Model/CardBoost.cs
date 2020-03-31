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

        //Hacer un level boost y hacer q nivel es y tener varios de un nivel para poder hacer arbol habilidades
//Fijafme si este lahout sirve o si dividir en dos tipos de boost pa runa y level
    }

Public class LevelBoost : IEntity {
public int Level { get; set; }
Public CardBoost Boost {get; set;}
Public string ImprovedName {get; set;}
}
}
