using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scanventory.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        public string Barcode { get; set; } = string.Empty;

        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int MinimumStock { get; set; } = 5;

        public DateTime CreatedDate { get; set; }
    }
}