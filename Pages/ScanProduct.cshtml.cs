using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Scanventory.Data;
using Scanventory.Models;

namespace Scanventory.Pages
{
    public class ScanProductModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ScanProductModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Barcode { get; set; } = string.Empty;

        [BindProperty]
        public int AdjustmentAmount { get; set; }

        [BindProperty]
        public string AdjustmentType { get; set; } = "add";

        public Product? FoundProduct { get; set; }

        public string? Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Barcode))
            {
                Message = "Please enter a barcode.";
                return Page();
            }

            FoundProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.Barcode == Barcode.Trim());

            if (FoundProduct == null)
            {
                Message = "No product was found with this barcode.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAdjustStockAsync()
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Barcode == Barcode.Trim());

            if (product == null)
            {
                Message = "Product not found.";
                return Page();
            }

            if (AdjustmentAmount <= 0)
            {
                Message = "Please enter an amount greater than 0.";
                FoundProduct = product;
                return Page();
            }

            if (AdjustmentType == "add")
            {
                product.Quantity += AdjustmentAmount;
                Message = $"Stock increased by {AdjustmentAmount}.";
            }
            else if (AdjustmentType == "remove")
            {
                if (AdjustmentAmount > product.Quantity)
                {
                    Message = "You cannot remove more stock than is currently available.";
                    FoundProduct = product;
                    return Page();
                }

                product.Quantity -= AdjustmentAmount;
                Message = $"Stock decreased by {AdjustmentAmount}.";
            }

            await _context.SaveChangesAsync();

            FoundProduct = product;

            return Page();
        }
    }
}