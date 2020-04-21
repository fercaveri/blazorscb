using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Passive
    {
        NONE = 0,
        POISON, //Veneno X danio cada X segundos
        PIERCING, //Atraviesa X defensa (resta el numero a la def)
        IGNORE_DEF, //Danio verdadero (no tiene en cuenta defensa)
        STUN, //Deja aturdido X segundos
        FREEZE, //Ralentiza X% por X segundos
        SHATTER, //Saca X% de vida total al golpear
        DOOM, //Mata luego de X segundos
        BLAZE, //Fuego X danio cada vez que lo golpean por X segundos
        BACKTRACK, //Retrocede X segundos de speed
        BOUNCE, //Rebota y hace X de danio a otro adversario
        BLEED, //Sangra al enemigo, que recibe X de danio por cada ataque realizado durante X segundos
        DODGE, //Evade el ataque con un X% de chances
        IMMUNE, //Ninguna pasiva le hace efecto
        TOUGH, //Un X% de reducir el danio un X
        GHOST, //No recibe danio directo al ser atacado, solo con efectos secundarios
        BLOWMARK, //Marca al objetivo por cada golpe, anadiendo X danio al proximo ataque
        BLIND, //Ciega al enemigo dejandole un X% de que missee durante X segundos
        DEVIANT, //El ataque es aleatorio entre el atk base y X
        ABLAZE, //Al golpearte, el enemigo recibe X de danio
        OBLIVION, //X% de matar al enemigo,
        
        //HACER ESTAS LOGICAS

        BURN, //Un X% de daniar al enemigo con Z de danio extra y luego de Y segundos dania nuevamente,
        BERSEKER, //Mitad de vida o menos, incrementa danio y velocidad
        KNOCKOUT, //Mata si la vida del enemigo es X o menor
        LIFESTEAL, //Se cura un X% del danio causado
        REFLECT, //Refleja estados en un X%,

        //NO USADO

        TORNADO, //Golpea X veces aleatoriamente
        DEFRAGMENTER, //Saca X% de vida actual al golpear

        //NO USADO NI ICONO

        SPIKE_ARMOR, //Devuelve el X% de danio al atacante
        REFINEMENT, //Incrementa en X el ataque por cada golpe dado
        DISPELL, //Elimina efectos negativos al curar --> tiene que usarse con heal
        INCAPACITATE, //Reduce el ataque del enemigo en X durante X segundos
        CLEAVE, //Dania en un X% a los enemigos del costado del atacado
        DISABLE, //Deja inactiva la carta por X segundos (no puede ser atacada ni atacar)
        VANISH, //Reduce en X el ataque propio por cada golpe hasta llegar a 0
        SURPRISE, //Golpea en X a pos2 en Y a pos3 y en Z a pos 4
        CONFUSSION, //La carta golpea al azar a cualquiera del tablero
        NULLIFY, //Mientras este vivo, ningun aliado puede sufrir efectos
        MARKDOWN, //Luego de X marcas dania por Y en el prox ataque
        INTENSIFY, //Incrementa el danio sufrido por efectos un X%
        TAUNT, //Al golpear hace q el enemigo solo pueda atacarlo a el durante X segundos
        SHIELD, //Brinda X de armadura (como otra barra de hp que no cuenta def para el danio)
        ASSAULT, //Por cada turno enemigo golpea al azar a uno causando X danio
        ELECTRIFY, //X% de perder el turno durante Y segundos
        FROZEN, //Immobilize the target but reduce damage taken durante X segundos
        POISONUS, //Envenena al enemigo que lo ataca, mismos parametros que poison
        ROUGH, //Reduce el danio del enemigo por cada golpe recibido en X

        //TODO: NO PASIVAS PERO PUEDE SER FUEGO Y POISON ALL o BLEED
    }
}
