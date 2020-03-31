using System;

namespace SurrealCB.Data.Model
{
    public class CardBoost : Stats
    {
        
        public CardPassive Passive { get; set; }
        //Si quiero mejorar una pasiva existente usar un nuevo cardpassive con el mismo id

        //Hacer un level boost y hacer q nivel es y tener varios de un nivel para poder hacer arbol habilidades
//Fijafme si este lahout sirve o si dividir en dos tipos de boost pa runa y level
    }

Public class LevelBoost : IEntity {
public int Level { get; set; }
Public CardBoost Boost {get; set;}
Public string ImprovedName {get; set;}
//Public List<RequiredObject> RequiredObjects {get; set;}
Public int Cost {get; set;}
}
}
