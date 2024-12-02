using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class NotFoundException : BaseApiException
    {
        public NotFoundException(string message = "Not Found") : base(404, message) { }
    }
}
