using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WorkerCategory : BaseEntity
    {
        public string WorkerId { get; set; }
        public Guid CategoryId { get; set; }
        public virtual User Worker { get; set; }
        public virtual Category Category { get; set; }
    }
}
