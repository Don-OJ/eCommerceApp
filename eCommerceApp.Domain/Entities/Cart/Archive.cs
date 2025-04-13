using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Domain.Entities.Cart
{
    public class Archive
    {
        // Unique identifier for the archive entry
        [Key]
        public Guid Id { get; set; }

        // Identifier for the user associated with the archive entry
        public string? UserId { get; set; }

        // Identifier for the product associated with the archive entry
        public Guid ProductId { get; set; }

        // Quantity of the product in the archive entry
        public int Quantity { get; set; }

        // Date and time when the archive entry was created
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
