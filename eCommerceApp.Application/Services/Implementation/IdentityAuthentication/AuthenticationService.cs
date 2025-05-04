using AutoMapper;
using eCommerceApp.Application.DTO.Identity;
using eCommerceApp.Application.DTO.Response;
using eCommerceApp.Application.Services.Interface.Authentication;
using eCommerceApp.Application.Services.Interface.Logging;
using eCommerceApp.Application.Validations;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interface.IdentityAuthentication;
using FluentValidation;
using System.Net;

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

            // --- FIX: Remove existing tokens for this user FIRST ---
            await tokenManagement.RemoveRefreshTokensByUserIdAsync(_user.Id);
            // -------------------------------------------------------

            // --- FIX: ALWAYS Add the new refresh token ---
            int addRefreshTokenResult = await tokenManagement.AddRefreshToken(_user.Id, refreshToken);
            // ---------------------------------------------

            // Check if the ADD operation failed
            return addRefreshTokenResult <= 0
                ? new LoginResponse { Message = "Login failed : Error occured saving token." }
                : new LoginResponse { Success = true, Token = jwtToken, RefreshToken = refreshToken, Message = "Login successful." };

            // Add refresh token to the user
            //int addRefreshTokenResult = await tokenManagement.AddRefreshToken(_user.Id, refreshToken);
            //return addRefreshTokenResult <= 0 ? new LoginResponse { Message = "Login failed : Error occured in login." } : new LoginResponse { Success = true, Token = jwtToken, RefreshToken = refreshToken, Message = "Login successful." };
        }

        // Method to revive a token using a refresh token
        // remove the used token and add the new one, instead of trying to "update" based on userId.
        public async Task<LoginResponse> ReviveToken(string refreshToken)
        {
            // --- FIX: Decode the incoming token ---
            // Use UrlDecode to reverse the extra layer of encoding.
            var decodedRefreshToken = WebUtility.UrlDecode(refreshToken);

            // Validate the refresh token
            // Validate the specific refresh token provided
            bool validTokenResult = await tokenManagement.ValidateRefreshToken(decodedRefreshToken);
            if (!validTokenResult) return new LoginResponse { Message = "Token revival failed : Invalid Refresh Token." };

            // Retrieve user ID by refresh token
            // Retrieve user ID associated with this specific token
            string userId = await tokenManagement.GetUserIdByRefreshToken(decodedRefreshToken);
            if (string.IsNullOrEmpty(userId)) // Check if userId was actually found
            {
                logger.LogWarning("ReviveToken: User ID not found for decoded token: " + $"{decodedRefreshToken}");
                return new LoginResponse { Message = "Token revival failed : User not found for token." };
            }

            // Retrieve the user by ID
            AppUser? user = await userManagement.GetUserById(userId);
            if (user == null) // Check if user still exists
            {
                logger.LogWarning("ReviveToken: User account not found for User ID " + $"{userId}");
                return new LoginResponse { Message = "Token revival failed : User account not found." };
            }

            // Retrieve user claims
            var claims = await userManagement.GetUserClaims(user!.Email!);

            // Generate new JWT token and refresh token
            string newJwtToken = tokenManagement.GenerateToken(claims);
            string newRefreshToken = tokenManagement.GetRefreshToken();

            // --- FIX: Remove the OLD used token ---
            await tokenManagement.RemoveRefreshTokenAsync(decodedRefreshToken); // Use the specific token value

            // --- FIX: Add the NEW refresh token for the user ---
            await tokenManagement.AddRefreshToken(userId, newRefreshToken); // AddRefreshToken should handle potential errors internally or return success/failure

            // Assuming AddRefreshToken was successful if no exception was thrown
            return new LoginResponse { Success = true, Token = newJwtToken, RefreshToken = newRefreshToken, Message = "Token revived successfully." };
        }
    }
}
