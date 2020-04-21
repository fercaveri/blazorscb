using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SurrealCB.Data.Model
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [MaxLength(64)]
        public string FirstName { get; set; }
        [MaxLength(64)]
        public string LastName { get; set; }
        [MaxLength(64)]
        public string FullName { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
        public virtual ICollection<ApiLogItem> ApiLogItems { get; set; }
        public virtual UserProfile Profile { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<PlayerCard> Cards { get; set; }
        public virtual ICollection<PlayerRune> Runes { get; set; }
    }
}
