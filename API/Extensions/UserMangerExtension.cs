using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace API.Extensions
{
    public static class UserMangerExtension
    {
        public static async Task<ApplicationUser?> GetUserByIdFromClaimsAsyn(this UserManager<ApplicationUser> value, ClaimsPrincipal user)
        {
            var userClaim = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (userClaim != null && int.TryParse(userClaim.Value, out int id))
            {
                return await value.FindByIdAsync(id.ToString());

            }

            return null;
        }
    }
}
