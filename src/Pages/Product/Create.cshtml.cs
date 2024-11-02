using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class CreateModel : PageModel
    {
        // Data Service
        public JsonFileProductService ProductService { get; }

        public CreateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // Collection of the Data
        [BindProperty]
        public ProductModel Product { get; set; } = new ProductModel
        {
            Cast = new List<string>()
        };

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Generate a new ID if creating a new product
            if (string.IsNullOrEmpty(Product.Id))
            {
                Product.Id = Guid.NewGuid().ToString(); // Generate a new unique ID
                ProductService.CreateNewProduct(Product); // Use CreateNewProduct method to save the new product
            }

            return RedirectToPage("./Index");
        }
    }
}
