using Core.Entities;
using Core.Extensions;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.SpecificationBuilder
{
    public class VacancySpecificationBuilder: BaseSpecificationBuilder<Vacancy, VacancySpecificationBuilder>
    {

        public VacancySpecificationBuilder WithEmployerId(int EmployerId)
        {

            _criteria = PredicateBuilder.And(_criteria, x => x.EmployerId == EmployerId);

            return this;
        }

        public VacancySpecificationBuilder WithName(string name)
        {

            _criteria = PredicateBuilder.And(_criteria, x => x.Name.Contains(name));

            return this;
        }

        public VacancySpecificationBuilder WithStatus(bool status) {

            _criteria = PredicateBuilder.And(_criteria, x => x.IsActive == status);


            return this;
        }

        public VacancySpecificationBuilder WithStartDate(string? startDate)
        {
            var dt = new DateTimeOffset();

            DateTimeOffset? dateParsed = dt.ParseStringDate(startDate);

            if (dateParsed != null)
            {

                _criteria = PredicateBuilder.And(_criteria, x => x.CreatedDate >= dateParsed);

            }
            return this;

        }

        public VacancySpecificationBuilder WithEndDate(string? endDate)
        {
            var dt = new DateTimeOffset();

            DateTimeOffset? dateParsed = dt.ParseStringDateToEndOfDay(endDate);

            if (dateParsed != null)
            {

                _criteria = PredicateBuilder.And(_criteria, x => x.CreatedDate <= dateParsed);

            }
            return this;

        }


    }
}
