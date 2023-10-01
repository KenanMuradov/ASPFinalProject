using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; } 
        public string Email { get; set; }
        public bool IsWorker { get; set; } = false;
        public string? CategoryId { get; set; }
    }
}
