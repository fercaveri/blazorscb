using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SurrealCB.Data.Model
{
    public class LevelBoost : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }
        public int Level { get; set; }
        public virtual CardBoost Boost { get; set; }
        public string ImprovedName { get; set; }
        public virtual List<RequiredItem> RequiredItems { get; set; }
        public int? RequiredBoostId { get; set; }
        public virtual LevelBoost RequiredBoost { get; set; }
        public int Cost { get; set; }
    }
}
