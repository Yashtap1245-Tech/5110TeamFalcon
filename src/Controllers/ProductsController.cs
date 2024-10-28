using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        public ProductsController(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        public JsonFileProductService ProductService { get; }

        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return ProductService.GetAllData();
        }

        [HttpPatch]
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            ProductService.AddRating(request.ProductId, request.Rating);
            
            return Ok();
        }
        /// <summary>
        /// Represents a request to submit a rating for a product. 
        /// This class encapsulates the necessary information for a rating submission, 
        /// including the product identifier and the rating value.
        /// </summary>
        public class RatingRequest
        {
            //ProductID Field
            public string ProductId { get; set; }
            //Rating Field
            public int Rating { get; set; }
        }
    }
}