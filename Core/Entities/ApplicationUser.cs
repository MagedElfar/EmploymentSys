using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ApplicationUser:IdentityUser<int>,IBaseEntity
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public IEnumerable<Vacancy> Vacancies { get; set; } = new List<Vacancy>();
        public IEnumerable<Application> Applications { get; set; } = new List<Application>();

    }
}
