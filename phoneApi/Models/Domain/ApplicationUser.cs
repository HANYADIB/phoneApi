using Microsoft.AspNetCore.Identity;

namespace phoneApi.Models.Domain
{
    public class ApplicationUser:IdentityUser
    {
        public string? Name { get; set; }
       // public string? Role { get; set; }

    }
}
