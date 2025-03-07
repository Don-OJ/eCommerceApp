using AutoMapper;
using eCommerceApp.Application.DTO.Identity;
using eCommerceApp.Application.DTO.Response;
using eCommerceApp.Application.Services.Interface.Authentication;
using eCommerceApp.Application.Services.Interface.Logging;
using eCommerceApp.Application.Validations;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interface.IdentityAuthentication;
using FluentValidation;

namespace eCommerceApp.Application.Services.Implementation.IdentityAuthentication
{
    // AuthenticationService class implementing IAuthenticationService
    public class AuthenticationService
        (ITokenManagement tokenManagement, IUserManagement userManagement,
        IRoleManagement roleManagement, IAppLogger<AuthenticationService> logger,
        IMapper mapper, IValidator<CreateUser> createUserValidator,
        IValidator<LoginUser> loginUserValidator, IValidationService validationService)
        : IAuthenticationService
    {
        // Method to create a new user
        public async Task<ServiceResponse> CreateUser(CreateUser user)
        {
            // Validate the user model
            var _validationReseult = await validationService.ValidateAsync(user, createUserValidator);
            if (!_validationReseult.Succcess) return _validationReseult;

            // Map the CreateUser DTO to AppUser entity
            var mappedModel = mapper.Map<AppUser>(user);
            mappedModel.UserName = user.Email; // Set the username as email
            mappedModel.PasswordHash = user.Password; // Set the password hash

            // Create the user
            var result = await userManagement.CreateUser(mappedModel);
            if (!result)
                return new ServiceResponse { Message = "User creation failed : Email Address might already be in use or unknown error occurred." };

            // Retrieve the created user by email
            var _user = await userManagement.GetUserByEmail(user.Email);
            // Retrieve all users
            var users = await userManagement.GetAllUsers();
            // Assign role to the user
            bool assignedResult = await roleManagement.AddUserToRole(_user!, users!.Count() > 1 ? "User" : "Admin");

            if (!assignedResult)
            {
                // Remove user if role assignment fails
                int removeUserResult = await userManagement.RemoveUserByEmail(_user!.Email!);
                if (removeUserResult <= 0)
                {
                    // Log error if user removal fails
                    logger.LogError(new Exception($"User with Email as {_user.Email} could not be removed after failed role assignment."), "User could not be assigned Role.");

                    return new ServiceResponse { Message = "User creation failed : Error occured in create account." };
                }
            }
            return new ServiceResponse { Succcess = true, Message = "User created successfully." };

            // Verify Email Address and send Email Verification Link to the User Email Address  
        }

        // Method to login a user
        public async Task<LoginResponse> LoginUser(LoginUser user)
        {
            // Validate the login model
            var _validationReseult = await validationService.ValidateAsync(user, loginUserValidator);
            if (!_validationReseult.Succcess) return new LoginResponse { Message = _validationReseult.Message };

            // Map the LoginUser DTO to AppUser entity
            var mappedModel = mapper.Map<AppUser>(user);
            mappedModel.PasswordHash = user.Password; // Set the password hash

            // Attempt to login the user
            bool loginResult = await userManagement.LoginUser(mappedModel);
            if (!loginResult) return new LoginResponse { Message = "Login failed : Invalid Email or Password." };

            // Retrieve the user by email
            var _user = await userManagement.GetUserByEmail(user.Email);
            // Retrieve user claims
            var claims = await userManagement.GetUserClaims(_user!.Email!);

            // Generate JWT token and refresh token
            string jwtToken = tokenManagement.GenerateToken(claims);
            string refreshToken = tokenManagement.GetRefreshToken();

            // Add refresh token to the user
            int addRefreshTokenResult = await tokenManagement.AddRefreshToken(_user.Id, refreshToken);
            return addRefreshTokenResult <= 0 ? new LoginResponse { Message = "Login failed : Error occured in login." } : new LoginResponse { Success=true, Token = jwtToken, RefreshToken = refreshToken, Message = "Login successful." };
        }

        // Method to revive a token using a refresh token
        public async Task<LoginResponse> ReviveToken(string refreshToken)
        {
            // Validate the refresh token
            bool validTokenResult = await tokenManagement.ValidateRefreshToken(refreshToken);
            if (!validTokenResult) return new LoginResponse { Message = "Token revival failed : Invalid Refresh Token." };

            // Retrieve user ID by refresh token
            string userId = await tokenManagement.GetUserIdByRefreshToken(refreshToken);
            // Retrieve the user by ID
            AppUser? user = await userManagement.GetUserById(userId);
            // Retrieve user claims
            var claims = await userManagement.GetUserClaims(user!.Email!);
            // Generate new JWT token and refresh token
            string newJwtToken = tokenManagement.GenerateToken(claims);
            string newRefreshToken = tokenManagement.GetRefreshToken();
            // Update the refresh token for the user
            await tokenManagement.UpdateRefreshToken(userId, newRefreshToken);

            return new LoginResponse { Token = newJwtToken, RefreshToken = newRefreshToken, Message = "Token revived successfully." };
        }
    }
}
