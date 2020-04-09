using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurrealCB.Data.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SurrealCB.CommonUI.Services
{
    public class ExpHelper
    {
        public static int GetNextLevelExp(int baseExp, int tier, int currentExp)
        {
            var level = 2;
            var levelExp = baseExp;
            while (currentExp > levelExp)
            {
                level++;
                levelExp = GetLevelExp(baseExp, tier, level);
            }
            return levelExp;
        }

        public static int GetLevelExp(int baseExp, int tier, int level)
        {
            var tierFormula = tier == 1 ? 1 : (double)tier / 1.7;
            var expNum = level == 2 ? 0.0 : (level - 1) / (5.0 / tierFormula);
            return (int)Math.Pow((double)baseExp, (expNum + 1));
        }
    }
}
