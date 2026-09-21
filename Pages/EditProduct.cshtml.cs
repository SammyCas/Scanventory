using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Scanventory.Data;
using Scanventory.Models;

namespace Scanventory.Pages
{
    public class EditProductModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditProductModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            Product = product;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var productToUpdate = await _context.Products.FindAsync(Product.ProductID);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            bool barcodeExists = await _context.Products
                .AnyAsync(p =>
                    p.Barcode == Product.Barcode &&
                    p.ProductID != Product.ProductID);

            if (barcodeExists)
            {
                ModelState.AddModelError(
                    "Product.Barcode",
                    "Another product already uses this barcode."
                );

                return Page();
            }

            productToUpdate.Barcode = Product.Barcode;
            productToUpdate.ProductName = Product.ProductName;
            productToUpdate.Category = Product.Category;
            productToUpdate.Price = Product.Price;
            productToUpdate.Quantity = Product.Quantity;
            productToUpdate.MinimumStock = Product.MinimumStock;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Inventory");
        }
    }
}