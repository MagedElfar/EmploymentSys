using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class AdbContext: IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public AdbContext(DbContextOptions options) :base(options)
        {

        }

        public override DbSet<ApplicationUser> Users {  get; set; }
   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
