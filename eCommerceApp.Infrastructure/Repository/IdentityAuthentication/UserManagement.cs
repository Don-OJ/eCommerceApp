using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interface.IdentityAuthentication;
using eCommerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eCommerceApp.Infrastructure.Repository.IdentityAuthentication
{
    public class UserManagement(IRoleManagement roleManagement, UserManager<AppUser> userManager, AppDbContext context) : IUserManagement // Defining the UserManagement class implementing IUserManagement
    {
        public async Task<bool> CreateUser(AppUser user) // Method to create a new user
        {
            if (user != null) // Check if user is not null
                return false; // Return false if user is not null

            return (await userManager.CreateAsync(user!, user!.PasswordHash!)).Succeeded; // Create user and return the result
        }

        public async Task<IEnumerable<AppUser>?> GetAllUsers() => await context.Users.ToListAsync(); // Method to get all users

        public async Task<AppUser?> GetUserByEmail(string email) => await userManager.FindByEmailAsync(email); // Method to get a user by email

        public async Task<AppUser> GetUserById(string id) // Method to get a user by ID
        {
            var user = await userManager.FindByIdAsync(id); // Find user by ID
            return user!; // Return the user
        }

        public async Task<List<Claim>> GetUserClaims(string email) // Method to get user claims by email
        {
            var _user = await GetUserByEmail(email); // Get user by email
            string? roleName = await roleManagement.GetUserRole(_user!.Email!); // Get user role by email

            List<Claim> claims = [ // Create a list of claims
                new Claim("Fullname", _user!.Fullname), // Add fullname claim
                    new Claim(ClaimTypes.NameIdentifier, _user!.Id), // Add name identifier claim
                    new Claim(ClaimTypes.Email, _user!.Email!), // Add email claim
                    new Claim(ClaimTypes.Role, roleName!) // Add role claim
                ];

            return claims; // Return the list of claims
        }

        public async Task<bool> LoginUser(AppUser user) // Method to log in a user
        {
            var _user = await GetUserByEmail(user.Email!); // Get user by email
            if (_user is null) return false; // Return false if user is null

            string? roleName = await roleManagement.GetUserRole(_user.Email!); // Get user role by email
            if (string.IsNullOrEmpty(roleName)) return false; // Return false if role name is null or empty

            return await userManager.CheckPasswordAsync(_user, user.PasswordHash!); // Check password and return the result
        }

        public async Task<int> RemoveUserByEmail(string email) // Method to remove a user by email
        {
            var user = await context.Users.FirstOrDefaultAsync(_ => _.Email == email); // Find user by email
            context.Users.Remove(user); // Remove the user
            return await context.SaveChangesAsync(); // Save changes and return the result
        }
    }
}
