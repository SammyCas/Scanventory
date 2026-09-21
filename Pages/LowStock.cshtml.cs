using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Scanventory.Data;
using Scanventory.Models;

namespace Scanventory.Pages
{
    public class LowStockModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LowStockModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; } = new();

        public async Task OnGetAsync()
        {
            Products = await _context.Products
                .Where(p => p.Quantity <= p.MinimumStock)
                .OrderBy(p => p.Quantity)
                .ThenBy(p => p.ProductName)
                .ToListAsync();
        }
    }
}