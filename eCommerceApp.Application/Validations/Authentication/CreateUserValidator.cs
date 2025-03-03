using eCommerceApp.Application.DTO.Identity;
using FluentValidation;

namespace eCommerceApp.Application.Validations.Authentication
{
    // Validator for creating a new user
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            // Rule for Fullname: must not be empty
            RuleFor(x => x.Fullname)
                .NotEmpty().WithMessage("Full name is required.");

            // Rule for Email: must not be empty and must be a valid email format
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required. ")
                .EmailAddress().WithMessage("Invalid email format.");

            // Rule for Password: must not be empty, must be at least 8 characters long,
            // must contain at least one uppercase letter, one lowercase letter, one number, and one special character
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required. ")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[^\w]").WithMessage("Password must contain at least one special character.");

            // Rule for ConfirmPassword: must match the Password
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match.");
        }
    }
}
