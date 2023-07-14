using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Azure.Core;
using Mentat.UI.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Mentat.UI.Areas.Identity
{
    public class UserRoleTypeRequirementHandler : AuthorizationHandler<UserRoleTypeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRoleTypeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                return Task.FromResult(0);
            }

            var role = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
            var match = requirement.RequiredTypes.FirstOrDefault(str => str == role);

            if(!string.IsNullOrEmpty(match))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
