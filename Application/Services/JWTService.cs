using Core.Entities;
using Core.Interfaces.Services;
using Core.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class JwtService : IJwtService
    {

        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly UserManager<ApplicationUser> userManager;

        public JwtService(
            IOptions<JwtOptions> jwtOptions,
            UserManager<ApplicationUser> userManager
            )
        {
            _jwtOptions = jwtOptions;
            this.userManager = userManager;
        }

        public async Task<String> GenerateToken(ApplicationUser user , IEnumerable<string>? roles =null)
        {

            var userClaims = await userManager.GetClaimsAsync(user);

            var roleClaims = new List<Claim>();

            if(roles != null)
            {
                foreach (var role in roles)
                    roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }


            var claims = new Claim[]
              {
                    new Claim(ClaimTypes.NameIdentifier ,user.Id.ToString() ),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              }
              .Union(userClaims)
              .Union(roleClaims);

            var tokenHandelr = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SigningKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

          
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Value.Issuer,
                //Audience = _jwtOptions.Value.Aduience,
                SigningCredentials = credentials,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtOptions.Value.Lifetime)),
            };

            var securityToken = tokenHandelr.CreateToken(securityTokenDescriptor);

            return tokenHandelr.WriteToken(securityToken);
        }
    }
}
