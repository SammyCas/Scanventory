using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scanventory.Data;
using Scanventory.Models;

namespace Scanventory.Pages
{
    public class DeleteProductModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteProductModel(ApplicationDbContext context)
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
            var product = await _context.Products.FindAsync(Product.ProductID);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Inventory");
        }
    }
}