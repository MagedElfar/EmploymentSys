using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class UserUnauthorizedException : BaseApiException
    {
        public UserUnauthorizedException(string message = "Unauthorized") : base(401, message)
        {
        }
    }
}
