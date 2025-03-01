using eCommerceApp.Domain.Entities.Identity;
using System.Security.Claims;

namespace eCommerceApp.Domain.Interface.IdentityAuthentication
{
    public interface IUserManagement // Defining the IUserManagement interface
    {
        Task<bool> CreateUser(AppUser user); // Method to create a new user
        Task<bool> LoginUser(AppUser user); // Method to log in a user
        Task<AppUser?> GetUserByEmail(string email); // Method to get a user by their email
        Task<AppUser> GetUserById(string id); // Method to get a user by their ID
        Task<IEnumerable<AppUser>?> GetAllUsers(); // Method to get all users
        Task<int> RemoveUserByEmail(string email); // Method to remove a user by their email
        Task<List<Claim>> GetUserClaims(string email); // Method to get the claims of a user by their email
    }
}
