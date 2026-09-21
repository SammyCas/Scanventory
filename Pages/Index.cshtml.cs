using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Scanventory.Data;

namespace Scanventory.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int TotalProducts { get; set; }

        public int LowStockProducts { get; set; }

        public int OutOfStockProducts { get; set; }

        public async Task OnGetAsync()
        {
            TotalProducts = await _context.Products.CountAsync();

            LowStockProducts = await _context.Products
                .CountAsync(p => p.Quantity > 0 &&
                                 p.Quantity <= p.MinimumStock);

            OutOfStockProducts = await _context.Products
                .CountAsync(p => p.Quantity == 0);
        }
    }
}