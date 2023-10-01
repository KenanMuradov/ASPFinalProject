using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {  
        public string? RefreshToken { get; set; }
        public float? Rating { get; set; }
        public virtual IEnumerable<WorkerCategory>? WorkerCategories { get; set; }
    }
}
