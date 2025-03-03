using eCommerceApp.Application.DTO.Response;
using FluentValidation;

namespace eCommerceApp.Application.Validations
{
    public class ValidationService : IValidationService
    {
        /// <summary>
        /// Validates the given model asynchronously using the specified validator.
        /// </summary>
        /// <typeparam name="T">The type of the model to validate.</typeparam>
        /// <param name="model">The model instance to validate.</param>
        /// <param name="validator">The validator instance to use for validation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a ServiceResponse indicating the validation result.</returns>
        public async Task<ServiceResponse> ValidateAsync<T>(T model, IValidator<T> validator)
        {
            // Perform the validation asynchronously
            var validationResult = await validator.ValidateAsync(model);

            // Check if the validation failed
            if (!validationResult.IsValid)
            {
                // Extract error messages from the validation result
                var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

                // Combine error messages into a single string
                string errorString = string.Join("; ", errors);

                // Return a ServiceResponse with the error messages
                return new ServiceResponse { Message = errorString };
            }
            else
            {
                // Return a successful ServiceResponse if validation passed
                return new ServiceResponse { Succcess = true };
            }
        }
    }
}
