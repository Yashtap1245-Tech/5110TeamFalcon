using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class DeleteModel : PageModel
    {
        // Data Service
        public JsonFileProductService ProductService { get; }

        public DeleteModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }
        // Collection of the Data
        [BindProperty]
        public ProductModel Product { get; set; }
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
}
