using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCrafts.WebSite.Pages.Product;

public class UpdateModel : PageModel
{
        // Data Service
        public JsonFileProductService ProductService { get; }
        
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
        public void OnGet(string id)
        {
            // Product = ProductService.GetDataForRead(id);
            Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));
        }

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