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
    public class AuthenticationService
        (ITokenManagement tokenManagement, IUserManagement userManagement, 
        IRoleManagement roleManagement, IAppLogger<AuthenticationService> logger,
        IMapper mapper, IValidator<CreateUser> createUserValidator, 
        IValidator<LoginUser> loginUserValidator, IValidationService validationService)
        : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUser(CreateUser user)
        {
            var _validationReseult = await validationService.ValidateAsync(user, createUserValidator);
            if (!_validationReseult.Succcess) return _validationReseult;

            var mappedModel = mapper.Map<AppUser>(user);
            mappedModel.UserName = user.Email;
            mappedModel.PasswordHash = user.Password;

            var result = await userManagement.CreateUser(mappedModel);
            if (!result)
                return new ServiceResponse { Message = "User creation failed : Email Address might already be in use or unknown error occurred." };

            var _user = await userManagement.GetUserByEmail(user.Email);
            var users = await userManagement.GetAllUsers();
            bool assignedResult = await roleManagement.AddUserToRole(_user!, users!.Count() > 1 ? "User" : "Admin");

            if (!assignedResult)
            {
                // remove user
                int removeUserResult = await userManagement.RemoveUserByEmail(_user!.Email!);
                if (removeUserResult <= 0)
                {
                    // error occurred while rolling back changes
                    // then log the error
                    logger.LogError(new Exception($"User with Email as {_user.Email} could not be removed after failed role assignment."), "User could not be assigned Role.");

                    return new ServiceResponse { Message = "User creation failed : Error occured in create account." };
                }
            }
            return new ServiceResponse { Succcess = true, Message = "User created successfully." };

           // Verify Email Addressd1234567890
           // +++++++++++++++++++++++++++++++
        }

        public async Task<LoginResponse> LoginUser(LoginUser user)
        {
            var _validationReseult = await validationService.ValidateAsync(user, loginUserValidator);
            if (!_validationReseult.Succcess) return new LoginResponse { Message = _validationReseult.Message };
        }

        public Task<LoginResponse> ReviveToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
