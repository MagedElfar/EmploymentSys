using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Application
{
    public class VacancyApplicationQueryDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "VacancyId must be grater than zero")]
        public int VacancyId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Limit should be more than 1")]
        public int? Limit { get; set; } = 10;

        [Range(1, int.MaxValue, ErrorMessage = "Page should be more than 1")]
        public int? Page { get; set; } = 1;

       public int EmployerId { get; set; }
    }
}
