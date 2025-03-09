using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interface.IdentityAuthentication;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eCommerceApp.Infrastructure.Repository.IdentityAuthentication
{
    public class TokenManagement(AppDbContext context, IConfiguration config) : ITokenManagement
    {
        // Method to add a refresh token for a user
        public async Task<int> AddRefreshToken(string userId, string refreshToken)
        {
            context.RefreshToken.Add(new RefreshToken
            {
                UserId = userId, // Setting user ID
                Token = refreshToken // Setting refresh token
            });

            return await context.SaveChangesAsync(); // Saving changes to the database
        }

        // Method to generate a JWT token
        public string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!)); // Getting the security key
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Creating signing credentials
            var expiration = DateTime.UtcNow.AddHours(2); // Setting token expiration time
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"], // Setting token issuer
                audience: config["Jwt:Audience"], // Setting token audience
                claims: claims, // Adding claims to the token
                expires: expiration, // Setting token expiration
                signingCredentials: cred // Adding signing credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token); // Writing and returning the token
        }

        // Method to generate a new refresh token
        public string GetRefreshToken()
        {
            const int byteSize = 64; // Setting byte size for the token
            byte[] randomBytes = new byte[byteSize]; // Creating a byte array
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes); // Filling the byte array with random bytes
            }
            string token = Convert.ToBase64String(randomBytes);
            return WebUtility.UrlEncode(token);
        }

        // Method to get user ID by refresh token
        public async Task<string> GetUserIdByRefreshToken(string refreshToken)
            => (await context.RefreshToken.FirstOrDefaultAsync(_ => _.Token == refreshToken))!.UserId;

        // Method to update a refresh token for a user
        public async Task<int> UpdateRefreshToken(string userId, string refreshToken)
        {
            var user = await context.RefreshToken.FirstOrDefaultAsync(_ => _.Token == refreshToken); // Finding the user by refresh token

            if (user == null) return -1; // If user not found, return -1

            user.Token = refreshToken; // Updating the refresh token
            return await context.SaveChangesAsync(); // Saving changes to the database
        }

        // Method to validate a refresh token
        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            var user = await context.RefreshToken.FirstOrDefaultAsync(_ => _.Token == refreshToken); // Finding the user by refresh token
            return user != null; // Returning true if user found, otherwise false
        }

        // Method to get user claims from a token
        public List<Claim> GetUserClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); // Creating a token handler
            var jwtToken = tokenHandler.ReadJwtToken(token); // Reading the JWT token
            if (jwtToken != null)
                return jwtToken.Claims.ToList(); // Returning the claims if token is valid
            else
                return new List<Claim>(); // Returning an empty list if token is invalid
        }
    }
}
