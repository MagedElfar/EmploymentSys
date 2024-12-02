using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Vacancy
{
    public class VacancyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxApplications { get; set; }
        public DateTimeOffset ExpiryDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int EmployerId { get; set; }
        public string EmployerName { get; set; }
    }
}
