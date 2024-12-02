using Core.Entities;
using Core.Extensions;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppicationEntity = Core.Entities.Application;

namespace Core.Specifications.SpecificationBuilder
{
    public class ApplicationpecificationBuilder: BaseSpecificationBuilder<AppicationEntity, ApplicationpecificationBuilder>
    {

        public ApplicationpecificationBuilder WithApplicantIdId(int applicantId)
        {

            _criteria = PredicateBuilder.And(_criteria, x => x.ApplicantId == applicantId);

            return this;
        }

        public ApplicationpecificationBuilder WithEmployerId(int employerId)
        {

            _criteria = PredicateBuilder.And(_criteria, x => x.Vacancy.EmployerId == employerId);

            return this;
        }

        public ApplicationpecificationBuilder WithVacancyId(int vacancyId)
        {

            _criteria = PredicateBuilder.And(_criteria, x => x.VacancyId == vacancyId);

            return this;
        }

     
     
        public ApplicationpecificationBuilder WithStartDate(string? startDate)
        {
            var dt = new DateTimeOffset();

            DateTimeOffset? dateParsed = dt.ParseStringDate(startDate);

            if (dateParsed != null)
            {

                _criteria = PredicateBuilder.And(_criteria, x => x.CreatedDate >= dateParsed);

            }
            return this;

        }

        public ApplicationpecificationBuilder WithEndDate(string? endDate)
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
