using System.ComponentModel.DataAnnotations;
using Scanventory.Models;
using Xunit;

namespace Scanventory.Tests;

public class ProductTests
{
    [Fact]
    public void ValidProduct_ShouldPassValidation()
    {
        var product = new Product
        {
            Barcode = "123456789",
            ProductName = "Test Product",
            Category = "Test Category",
            Price = 25.99m,
            Quantity = 10,
            MinimumStock = 5,
            CreatedDate = DateTime.Now
        };

        var context = new ValidationContext(product);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(
            product,
            context,
            results,
            true);

        Assert.True(isValid);
    }

    [Fact]
    public void ProductWithoutBarcode_ShouldFailValidation()
    {
        var product = new Product
        {
            Barcode = "",
            ProductName = "Test Product",
            Category = "Test Category",
            Price = 25.99m,
            Quantity = 10,
            MinimumStock = 5,
            CreatedDate = DateTime.Now
        };

        var context = new ValidationContext(product);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(
            product,
            context,
            results,
            true);

        Assert.False(isValid);
    }
}