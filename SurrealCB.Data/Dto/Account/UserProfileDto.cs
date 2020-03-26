using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SurrealCB.Data.Dto.Account
{
    public class UserProfileDto
    {
        [Key]
        public Guid UserId { get; set; }
        public long Id { get; set; }
        [Required]
        public string LastPageVisited { get; set; } = "/";
        public bool IsNavOpen { get; set; } = true;
        public bool IsNavMinified { get; set; } = false;
        public int Count { get; set; } = 0;
    }
}
