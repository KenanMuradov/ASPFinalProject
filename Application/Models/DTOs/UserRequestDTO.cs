using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs
{
    public class UserRequestDTO
    {
        public string Id { get; set; }
        public string WorkerEmail { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsCompleted { get; set; }
    }
}
