using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SurrealCB.Data.Model;

namespace SurrealCB.Data.Shared
{
    public interface IUserSession
    {
        Guid UserId { get; set; }
        int TenantId { get; set; }
        List<string> Roles { get; set; }
        string UserName { get; set; }
        bool DisableTenantFilter { get; set; }
    }

    public class UserSession : IUserSession
    {
        public bool IsAuthenticated { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int TenantId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
        public List<KeyValuePair<string, string>> ExposedClaims { get; set; }
        public bool DisableTenantFilter { get; set; }

        public UserSession() { }

        public UserSession(ApplicationUser user)
        {
            UserId = user.Id;
            UserName = user.UserName;
        }
    }
}
