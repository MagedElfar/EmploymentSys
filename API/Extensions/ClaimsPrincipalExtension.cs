using System.Security.Claims;

namespace API.Extensions
{
    public static class  ClaimsPrincipalExtension
    {
        public static int GetUserId(this ClaimsPrincipal value) {

            var claimId = value.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if(claimId != null && int.TryParse(claimId.Value , out int userId))
            {
                return userId;
            }

            return 0;
        }

        public static IEnumerable<string> GetUserRoles(this ClaimsPrincipal value)
        {

            var roles = value.Claims
                 .Where(c => c.Type == ClaimTypes.Role)
                 .Select(c => c.Value).ToList();

            return roles;
        }
    }
}
