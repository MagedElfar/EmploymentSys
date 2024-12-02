using Core.DTOS.Shared;
using Core.Specifications.SpecificationBuilder;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.User
{
    public class UserQueryDto:BaseSearchQueryDto
    {

        [EmailAddress]
        public string? Email { get; set; }
        public UserSpecificationBuilder BuildSpecification()
        {
            var builder = new UserSpecificationBuilder();

            if(Email != null) 
                builder.WithUserEmail(Email);

            return builder;

        }
    }
}
