using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class InternalServerErrorException : BaseApiException
    {
        public InternalServerErrorException(string message = "Internal Server Error") : base(500 , message)
        {
        }
    }
}
