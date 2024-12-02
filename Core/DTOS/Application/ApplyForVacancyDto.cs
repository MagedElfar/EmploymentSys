using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Application
{
    public class ApplyForVacancyDto
    {
        [Range(1 , int.MaxValue , ErrorMessage = "VacancyId must be grater than zero")]
        public int VacancyId { get; set; }
    }
}
