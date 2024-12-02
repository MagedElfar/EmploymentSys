using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Application:BaseEntity
    {
        public int ApplicantId { get; set; }
        public int VacancyId { get; set; }
        public ApplicationUser Applicant { get; set; }
        public Vacancy Vacancy { get; set; }
    }
}
