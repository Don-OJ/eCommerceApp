using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interface.IdentityAuthentication;
using Microsoft.AspNetCore.Identity;

namespace eCommerceApp.Infrastructure.Repository.IdentityAuthentication
{
    public class RoleManagement(UserManager<AppUser> userManager) : IRoleManagement
    {
        public async Task<bool> AddUserToRole(AppUser user, string roleName) =>
            (await userManager.AddToRoleAsync(user, roleName)).Succeeded;

        public async Task<string?> GetUserRole(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);
            return (await userManager.GetRolesAsync(user!)).FirstOrDefault();
        }
    }
}
