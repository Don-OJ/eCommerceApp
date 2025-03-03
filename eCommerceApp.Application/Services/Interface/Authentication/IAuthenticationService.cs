using eCommerceApp.Application.DTO.Identity;
using eCommerceApp.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Services.Interface.Authentication
{
    public interface IAuthenticationService
    {
        // Method to create a new user
        Task<ServiceResponse> CreateUser(CreateUser user);

        // Method to login a user
        Task<LoginResponse> LoginUser(LoginUser user);

        // Method to revive a token using a refresh token
        Task<LoginResponse> ReviveToken(string refreshToken);
    }
}
