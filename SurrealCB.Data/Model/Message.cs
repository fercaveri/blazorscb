using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SurrealCB.Data.Model
{
    public class Message : IEntity
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime MessageDate { get; set; }
        public virtual ApplicationUser Sender { get; set; }
    }
}
