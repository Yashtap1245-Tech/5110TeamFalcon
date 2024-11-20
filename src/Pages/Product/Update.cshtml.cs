using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCrafts.WebSite.Pages.Product
{

    /// <summary>
    /// Update Page will return all the data in an editable fields. 
    /// </summary>
    public class UpdateModel : PageModel
    {

        // Data Service
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// UpdateModel constructor
        /// </summary>
        /// <param name="productService"></param>
        public UpdateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // Collection of the Data
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// REST OnGet
        /// Return all the data
        /// </summary>
        public IActionResult OnGet(string id)
        // public void OnGet(string id)
        {
            // Product = ProductService.GetDataForRead(id);
            Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));
            if (Product == null)
            {
                return RedirectToPage("./IdNotFound");
            }

            return Page();
        }

        /// <summary>
        ///     Handles POST requests for the page
        /// </summary>
        /// <returns>
        ///     If validation fails, reload the current page to display validation errors
        ///     Redirect to the Index page after a successful update
        /// </returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProductService.UpdateData(Product);
            return RedirectToPage("./Index");
        }

    }

}