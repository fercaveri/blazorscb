using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealCB.Data.Model
{
    public abstract class IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
