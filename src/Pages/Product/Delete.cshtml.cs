using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Product
{

    /// <summary>
    /// Class DeleteModel inherits the PageModel class
    /// </summary>
    public class DeleteModel : PageModel
    {

        // Data Service
        public JsonFileProductService ProductService { get; }

        //DeleteModel Constructor
        public DeleteModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // Collection of the Data
        [BindProperty]
        public ProductModel Product { get; set; }
        
        /// <summary>
        /// OnGet Method with id as parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult OnGet(string id)
        {

            // Product = ProductService.GetDataForRead(id);
            Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));
            if (Product == null)
            {
                return RedirectToPage("./IdNotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProductService.DeleteData(Product.Id);
            return RedirectToPage("./Index");
        }

    }

}