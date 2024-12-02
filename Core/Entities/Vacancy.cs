using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Vacancy:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxApplications { get; set; }
        public DateTimeOffset ExpiryDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int EmployerId { get; set; }

        public bool IsArchived { get; set; } = false; 

        public ApplicationUser Employer { get; set; }
        public IEnumerable<Application> Applications { get; set; } = new List<Application>();
    }
}
