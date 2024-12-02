using Core.DTOS.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationDto> Login(LoaginDto loaginDto);
        Task<AuthenticationDto> Register(RegisterDto registerDto);
    }
}
