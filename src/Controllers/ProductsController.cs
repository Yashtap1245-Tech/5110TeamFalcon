using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Controllers
{
    /// <summary>
    /// Controller for managing products in the application. 
    /// This class provides API endpoints for retrieving product data and submitting ratings.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        ///Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productService">The service used to manage product data.</param>
        public ProductsController(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Gets the product data.
        /// This endpoint returns a collection of all products available in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return ProductService.GetAllData();
        }

        /// <summary>
        /// Submits a rating for a specific product.
        /// This endpoint accepts a rating request containing the product ID and the rating value,
        /// updates the product's rating in the data store, and returns a success response.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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