using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs
{
    public class WorkRequestDTO
    {
        public string UserEmail { get; set; }
        public string WorkerEmail { get; set; }
        public string Message { get; set; }
    }
}
