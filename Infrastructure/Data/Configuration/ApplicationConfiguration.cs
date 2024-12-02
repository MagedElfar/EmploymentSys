using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configuration
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.ToTable("applications")
                .HasKey(e => new {e.ApplicantId , e.VacancyId});

            builder.Ignore(x => x.Id);

            builder.HasOne(a => a.Vacancy)
             .WithMany(v => v.Applications)
             .HasForeignKey(a => a.VacancyId)
             .OnDelete(DeleteBehavior.Restrict); // Restrict deletion if there are related applications

            builder.HasOne(a => a.Applicant)
                .WithMany(u => u.Applications)
                .HasForeignKey(a => a.ApplicantId);
        }
    }
}
