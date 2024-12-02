using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.User
{
    public class UpdateUserDto
    {
        [Required]
        public string Email {  get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }   
        [Required]
        public string LastName { get; set; }
    }
}
