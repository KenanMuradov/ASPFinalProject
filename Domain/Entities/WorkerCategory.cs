using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WorkerCategory : BaseEntity
    {
        public string UserId { get; set; }
        public Guid CategoryId { get; set; }
        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
    }
}
