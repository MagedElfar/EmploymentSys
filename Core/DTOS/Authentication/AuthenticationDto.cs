using Core.DTOS.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Authentication
{
    public class AuthenticationDto:UserDto
    {
        public string Token { get; set; }
    }
}
