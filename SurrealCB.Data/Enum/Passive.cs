using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Passive
    {
        POISON = 0, //Veneno X danio cada X segundos
        PIERCING, //Atraviesa X defensa (resta el numero a la def)
        IGNORE_DEF, //Danio verdadero (no tiene en cuenta defensa)
        STUN, //Deja aturdido X segundos
        FREEZE, //Ralentiza X% por X segundos
        HP_SCATTER, //Saca X% de vida total al golpear
        DOOM, //Mata luego de X segundos
        BLAZE, //Fuego X danio cada vez que toca golpear
        BACKTRACK, //Retrocede X segundos de speed

        //TODAVIA EN PROCESO
        HP_DEFRAGMENTER, //Saca X% de vida actual al golpear
        SPIKE_ARMOR, //Devuelve el X% de danio al atacante
        BERSEKER, //Incrementa en X el ataque por cada golpe dado
        BLEED, //Sangra al enemigo, que recibe X de danio por cada ataque durante X segundos
        SHATTER, //Reduce el ataque del enemigo en X durante X segundos
    }
}
