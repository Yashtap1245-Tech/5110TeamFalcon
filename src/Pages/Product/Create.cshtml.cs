using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace ContosoCrafts.WebSite.Pages.Product
{

    /// <summary>
    /// Create Page will return a form with fields to create new data
    /// </summary>
    public class CreateModel : PageModel
    {

        // Data Service
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="productService"></param>
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

        /// <summary>
        ///     Handles POST requests for the page
        /// </summary>
        /// <returns>
        ///     If validation fails, reload the current page to display validation errors
        ///     Redirect to the Index page after a successful create
        /// </returns>
        public IActionResult OnPost()
        {
            // Check if ModelState is inValid
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