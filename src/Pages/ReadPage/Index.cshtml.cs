using System.Collections.Generic;
using System.Linq;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.ReadPage
{
    public class ReadPageModel : PageModel
    {
        // Data middletier
        public JsonFileReadService ProductService { get; }
        // public string MovieTitle { get; set; }
        // public string MoviePosterUrl { get; set; }
        // public string Description { get; set; }
        // public string CaseDetails { get; set; }
        // public double Rating { get; set; } // Use float or decimal if preferred
        
        public ReadPageModel(JsonFileReadService productService) {
            ProductService = productService;
        }
        
        // public IEnumerable<ProductModel> Products { get; private set; }
        // The data to show
        // public ProductModel Product;
        public ReadModel Product;
       
        public void OnGet(string id)
        {
            Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));
        }
    }
}