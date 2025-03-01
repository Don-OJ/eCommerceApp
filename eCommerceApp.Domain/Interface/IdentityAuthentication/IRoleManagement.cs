using eCommerceApp.Domain.Entities.Identity;

namespace eCommerceApp.Domain.Interface.IdentityAuthentication
{
    public interface IRoleManagement // Defining the IRoleManagement interface
    {
        Task<string?> GetUserRole(string userEmail); // Method to get the role of a user by their email
        Task<bool> AddUserToRole(AppUser user, string roleName); // Method to add a user to a specific role
    }
}
