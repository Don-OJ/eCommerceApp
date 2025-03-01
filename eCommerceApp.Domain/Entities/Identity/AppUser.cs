using Microsoft.AspNetCore.Identity;

namespace eCommerceApp.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsBoss { get; set; } = false;
    }
}
