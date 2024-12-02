using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Application
{
    public class ApplicationDto
    {
        public int VacancyId { get; set; }
        public string VacancyName { get; set; }

        public int EmployerId {  get; set; }
        public int ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantEmail { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

    }
}
