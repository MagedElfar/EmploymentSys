using Core.Entities;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.SpecificationBuilder
{
    public class UserSpecificationBuilder:BaseSpecificationBuilder<ApplicationUser, UserSpecificationBuilder>
    {

        public UserSpecificationBuilder WithUserEmail(string email)
        {

            _criteria = PredicateBuilder.And(_criteria, x => x.Email == email);

            return this;
        }

    }
}
