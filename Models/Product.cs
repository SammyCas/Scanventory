using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scanventory.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Barcode is required.")]
        public string Barcode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product name is required.")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; } = string.Empty;

        [Range(0, 99999999.99, ErrorMessage = "Price cannot be negative.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Minimum stock cannot be negative.")]
        public int MinimumStock { get; set; } = 5;

        public DateTime CreatedDate { get; set; }
    }
}