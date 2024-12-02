

using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces.Services
{
    public interface IJwtService
    {
        Task<String> GenerateToken(ApplicationUser user , IEnumerable<string>? roles = null);
    }
}
