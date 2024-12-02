using Core.Attributes;
using Core.DTOS.Shared;
using Core.Enums;
using Core.Specifications.SpecificationBuilder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Vacancy
{
    public class VacancyQuaryDto: BaseSearchQueryDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than 0")]
        public int? EmployerId { get; set; }

        [EnumStringValidation(typeof(VacancyOrderBy))]
        public string? Sort { get; set; }

        public bool? Status { get; set; }

        public string? Name { get; set; }
        public VacancySpecificationBuilder BuildSpecification()
        {
            var builder = new VacancySpecificationBuilder();

     
            if (!string.IsNullOrEmpty(ToDate))
            {
                builder.WithEndDate(ToDate);
            }

            if (!string.IsNullOrEmpty(FromDate))
            {
                builder.WithStartDate(FromDate);
            }

            if (EmployerId != null)
            {
                builder.WithEmployerId(EmployerId.Value);
            }

            if (Status != null)
            {
                builder.WithStatus(Status.Value);
            }


            if (!string.IsNullOrEmpty(Sort))
            {
                builder.WithOrderBy(Sort, Asc ?? true);
            }

            if (!string.IsNullOrEmpty(Name))
            {
                builder.WithName(Name);
            }

            return builder;
        }
    }
}
