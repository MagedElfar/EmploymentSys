using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class ConflictException : BaseApiException
    {
        public ConflictException(string message = "Conflict") : base(409, message) { }

    }
}
