using FluentNHibernate.Automapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealCB.Data.Repository
{
    public class SCBAutomappingConfiguration : DefaultAutomappingConfiguration
    {
        readonly string[] names = { "Card", "CardBoost", "CardPassive", "LevelBoost", "StatBoost", "PassiveBoost", "ApplicationUser", "UserProfile", "ApiLogItem", "Message",
        "PlayerCard", "Rune", "ActiveLevelBoost", "PlayerRune", "EnemyNpc", "Reward", "Map", "ActiveEffect", "BattleStatus", "BattleAction", "BattleCard", "Item", "Recipe",
        "CardRecipe", "ItemRecipe", "RuneRecipe", "RequiredItem", "Stats", "CardMetric", "RequiredItem"};
        public override bool ShouldMap(Type type)
        {
            //return type.Namespace.Equals("SurrealCB.Data.Model");
            return this.names.Contains(type.Name);
        }
    }
}
