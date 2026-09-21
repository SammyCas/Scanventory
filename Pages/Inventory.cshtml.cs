using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Scanventory.Data;
using Scanventory.Models;

namespace Scanventory.Pages
{
    public class InventoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public InventoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? CategoryFilter { get; set; }

        public List<string> Categories { get; set; } = new();

        public async Task OnGetAsync()
        {
            Categories = await _context.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                query = query.Where(p =>
                    p.ProductName.Contains(SearchTerm) ||
                    p.Barcode.Contains(SearchTerm) ||
                    p.Category.Contains(SearchTerm));
            }

            if (!string.IsNullOrWhiteSpace(CategoryFilter))
            {
                query = query.Where(p => p.Category == CategoryFilter);
            }

            Products = await query
                .OrderBy(p => p.ProductName)
                .ToListAsync();
        }
    }
}