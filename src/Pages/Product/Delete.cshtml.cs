using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Product
{

    /// <summary>
    /// Class DeleteModel inherits the class PageModel
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
        /// OnGet Method with id as parameters
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

        /// <summary>
        /// OnPost method after deleting the item
        /// </summary>
        /// <returns></returns>
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