using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WorkRequest : BaseEntity
    {
        public string WorkerId { get; set; }
        public string ClientId { get; set; }
        public string Message { get; set; }
        public bool? IsAccepted { get; set; }
        public bool IsCompleted { get; set; }
    }
}
