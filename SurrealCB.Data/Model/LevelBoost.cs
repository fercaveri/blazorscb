using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SurrealCB.Data.Model
{
    public class LevelBoost : Stats
    {
        public int Level { get; set; }
        public virtual CardBoost Boost { get; set; }
        public int BoostId { get; set; }
        public string ImprovedName { get; set; }
        //public virtual ICollection<RequiredItem> RequiredItems { get; set; }
        public int? RequiredBoostId { get; set; }
        public virtual LevelBoost RequiredBoost { get; set; }
        public int Cost { get; set; }
    }
}
