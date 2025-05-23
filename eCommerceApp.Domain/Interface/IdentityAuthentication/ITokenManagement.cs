﻿using System.Security.Claims;

namespace eCommerceApp.Domain.Interface.IdentityAuthentication
{
    public interface ITokenManagement // Defining the ITokenManagement interface
    {
        string GetRefreshToken(); // Method to get a refresh token
        List<Claim> GetUserClaimsFromToken(string token); // Method to get user claims from a token
        Task<bool> ValidateRefreshToken(string refreshToken); // Method to validate a refresh token
        Task<string> GetUserIdByRefreshToken(string refreshToken); // Method to get a user ID by a refresh token
        Task<int> AddRefreshToken(string userId, string refreshToken); // Method to add a refresh token for a user
        Task<int> UpdateRefreshToken(string userId, string refreshToken);// Method to update a refresh token for a user
        string GenerateToken(List<Claim> claims); // Method to generate a token based on a list of claims

        // You need a way to remove tokens based on the user ID
        Task<int> RemoveRefreshTokensByUserIdAsync(string userId); // Add this
        Task<int> RemoveRefreshTokenAsync(string refreshToken); // Add this for ReviveToken fix
    }
}
