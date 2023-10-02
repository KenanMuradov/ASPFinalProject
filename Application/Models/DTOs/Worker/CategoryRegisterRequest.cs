using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.Worker
{
    public class CategoryRegisterRequest
    {
        public string WorkerEmail { get; set; }
        public string CategoryId { get; set; }
    }
}
