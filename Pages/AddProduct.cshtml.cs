using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Scanventory.Data;
using Scanventory.Models;

namespace Scanventory.Pages
{
    public class AddProductModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddProductModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = new();

        public string? SuccessMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool barcodeExists = await _context.Products
                .AnyAsync(p => p.Barcode == Product.Barcode);

            if (barcodeExists)
            {
                ModelState.AddModelError(
                    "Product.Barcode",
                    "A product with this barcode already exists."
                );

                return Page();
            }

            _context.Products.Add(Product);

            await _context.SaveChangesAsync();

            SuccessMessage = "Product added successfully!";

            Product = new Product();

            ModelState.Clear();

            return Page();
        }
    }
}