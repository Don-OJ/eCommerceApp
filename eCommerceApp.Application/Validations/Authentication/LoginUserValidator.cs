using eCommerceApp.Application.DTO.Identity;
using FluentValidation;

namespace eCommerceApp.Application.Validations.Authentication
{
    // Validator for user login
    public class LoginUserValidator : AbstractValidator<LoginUser>
    {
        public LoginUserValidator()
        {
            // Rule for Email: must not be empty and must be a valid email format
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required. ")
                .EmailAddress().WithMessage("Invalid email format.");

            // Rule for Password: must not be empty
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required. ");
        }
    }
}
