using eCommerceApp.Application.DTO.Response;
using FluentValidation;

namespace eCommerceApp.Application.Validations
{
    public interface IValidationService
    {
        /// <summary>
        /// Validates the given model asynchronously using the specified validator.
        /// </summary>
        /// <typeparam name="T">The type of the model to validate.</typeparam>
        /// <param name="model">The model instance to validate.</param>
        /// <param name="validator">The validator instance to use for validation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a ServiceResponse indicating the validation result.</returns>
        Task<ServiceResponse> ValidateAsync<T>(T model, IValidator<T> validator);
    }
}
