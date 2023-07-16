using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Mentat.UI.Areas.Identity
{
    public class UserRoleTypeRequirement : IAuthorizationRequirement
    {
        public IEnumerable<string> RequiredTypes { get; private set; }

        public UserRoleTypeRequirement(params string []type)
        {
            RequiredTypes = type.ToList<string>();
        }
    }
}
