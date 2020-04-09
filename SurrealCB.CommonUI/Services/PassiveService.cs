using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurrealCB.Data.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SurrealCB.CommonUI.Services
{
    public class PassiveService
    {
        public static Tuple<string, string> GetPassiveTuple(Passive p, double p1 = 0, double p2 = 0, double p3 = 0) =>
        p switch
        {
            Passive.BLAZE => Tuple.Create($"Inflicts {p1} dmg every card turn for {p2} seconds.", "blaze"),
            Passive.DOOM => Tuple.Create($"Kill the card in {p1} seconds.", "doom"),
            Passive.FREEZE => Tuple.Create($"Slows the speed {p1} for {p2} seconds.", ""),
            Passive.HP_SHATTER => Tuple.Create($"Reduce {p1} of enemy total hp on every hit.", "hp_shatter"),
            Passive.HP_DEFRAGMENTER => Tuple.Create($"Reduce {p1} of enemy current hp on every hit.", "hp_defragmenter"),
            Passive.IGNORE_DEF => Tuple.Create($"Ignore entire enemy defense.", "ignore_defense"),
            Passive.PIERCING => Tuple.Create($"Ignore {p1} of defense on attack.", "piercing"),
            Passive.POISON => Tuple.Create($"Inflicts {p1} dmg every {p2} seconds for {p3} seconds.", "poison"),
            Passive.STUN => Tuple.Create($"Stun the target for {p1} seconds.", ""),
            Passive.BACKTRACK => Tuple.Create($"Backtrack the target speed for {p1} seconds.", "backtrack"),
            Passive.DODGE => Tuple.Create($"{p1}% chance of evasion.", "dodge"),
            _ => Tuple.Create("", ""),
        };
    }
}
