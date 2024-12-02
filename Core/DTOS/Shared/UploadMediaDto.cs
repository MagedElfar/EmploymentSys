using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Shared
{
    public class UploadMediaDto
    {
        public IFormFile File { get; set; }

    }
}
