using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCrafts.WebSite.Pages.Product
{

    /// <summary>
    /// Index Page will return all the data to show the user
    /// </summary>
    public class ReadModel : PageModel
    {

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="productService"></param>
        public ReadModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // Data Service
        public JsonFileProductService ProductService { get; }

        // Collection of the Data
        public ProductModel Product { get; private set; }

        /// <summary>
        /// REST OnGet
        /// Return all the data
        /// </summary>
        public IActionResult OnGet(string id)
        {
            Product = ProductService.GetDataForRead(id);
            if (Product == null)
            {
                return RedirectToPage("./IdNotFound");
            }

            return Page();
        }

    }

}