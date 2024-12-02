using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AdbContextSeed
    {

        public static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var roles = new List<ApplicationRole>
                {
                    new ApplicationRole
                    {
                        Name = UserRole.Employer.ToString()
                    },
                    new ApplicationRole
                    {
                        Name = UserRole.Applicant.ToString()
                    }
                };

                foreach (var role in roles)
                {
                    {
                        await roleManager.CreateAsync(role);
                    }
                }

            }
        }

    }
}
