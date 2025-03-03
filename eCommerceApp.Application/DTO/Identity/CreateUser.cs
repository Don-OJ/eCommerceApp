namespace eCommerceApp.Application.DTO.Identity
{
    // DTO class for creating a new user
    public class CreateUser : BaseModel
    {
        // Full name of the user
        public required string Fullname { get; set; } = string.Empty;

        // Confirmation of the user's password
        public required string ConfirmPassword { get; set; } = string.Empty;

        // Address of the user
        public string Address { get; set; } = string.Empty;

        // Flag to indicate if the user is a boss
        public bool IsBoss { get; set; } = false;
    }
}
