using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using ContosoCrafts.WebSite.Models;

using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    /// <summary>
    /// Service for managing product data stored in a JSON file.
    /// This class provides methods to retrieve, add, update, and delete product data.
    /// </summary>
    public class JsonFileProductService
    {
        /// <summary>
        /// Initializes a new instance of a class
        /// </summary>
        /// <param name="webHostEnvironment">The web hosting environment providing access to the web root path.</param>
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        /// <summary>
        /// Gets the web hosting environment for accessing the web root path.
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }
        /// <summary>
        /// Gets the path of the JSON file used to store product data.
        /// </summary>
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }
        /// <summary>
        /// Retrieves all product data from the JSON file.
        /// </summary>
        /// <returns>An enumerable collection representing all products</returns>
        public IEnumerable<ProductModel> GetAllData()
        {
            using(var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }
        /// <summary>
        /// Retrieves a specific product's data for reading, based on the product ID.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>The model which represents the product</returns>
        public ProductModel GetDataForRead(string id)
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }).FirstOrDefault(m => m.Id == id);
            }
        }

        /// <summary>
        /// Add Rating
        /// 
        /// Take in the product ID and the rating
        /// If the rating does not exist, add it
        /// Save the update
        /// </summary>
        /// Adds a rating to a specific product.
        /// If the rating does not already exist, it adds the rating and saves the updated product data.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to rate.</param>
        /// <param name="rating">The rating value to add (must be between 0 and 5).</param>
        public bool AddRating(string productId, int rating)
        {
            // If the ProductID is invalid, return
            if (string.IsNullOrEmpty(productId))
            {
                return false;
            }

            var products = GetAllData();

            // Look up the product, if it does not exist, return
            var data = products.FirstOrDefault(x => x.Id.Equals(productId));
            if (data == null)
            {
                return false;
            }

            // Check Rating for boundaries, do not allow ratings below 0
            if (rating < 0)
            {
                return false;
            }

            // Check Rating for boundaries, do not allow ratings above 5
            if (rating > 5)
            {
                return false;
            }

            // Check to see if the rating exist, if there are none, then create the array
            if(data.Ratings == null)
            {
                data.Ratings = new int[] { };
            }

            // Add the Rating to the Array
            var ratings = data.Ratings.ToList();
            ratings.Add(rating);
            data.Ratings = ratings.ToArray();

            // Save the data back to the data store
            SaveData(products);

            return true;
        }

        /// <summary>
        /// Find the data record
        /// Update the fields
        /// Save to the data store
        /// </summary>
        /// <param name="data">The model containing the updated product data</param>
        /// <returns>The updated model if successful otherwise null.</returns>
        public ProductModel UpdateData(ProductModel data)
        {
            var products = GetAllData();
            var productData = products.FirstOrDefault(x => x.Id.Equals(data.Id));
            if (productData == null)
            {
                return null;
            }

            // Update the data to the new passed in values
            productData.Title = data.Title;
            productData.Image = data.Image;
            productData.Description = data.Description.Trim();
            productData.Genre = data.Genre;
            productData.YouTubeID = data.YouTubeID;
            productData.Director = data.Director;
            // for (var i = 0; i < productData.Cast.Count; i++)
            // {
            //     productData.Cast[i] = data.Cast[i];
            // }
            productData.Cast = data.Cast;
            // productData.Url = data.Url;
            // productData.Image = data.Image;

            // productData.Quantity = data.Quantity;
            // productData.Price = data.Price;

            // productData.CommentList = data.CommentList;

            SaveData(products);

            return productData;
        }

        /// <summary>
        /// Save All products data to storage
        /// </summary>
        /// <param name="products">The collection of products to save.</param>
        private void SaveData(IEnumerable<ProductModel> products)
        {

            using (var outputStream = File.Create(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<ProductModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    products
                );
            }
        }

        /// <summary>
        /// Create a new product using default values
        /// After create the user can update to set values
        /// </summary>
        /// <returns>The newly created model</returns>
        public ProductModel CreateData()
        {
            var data = new ProductModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                Title = "Enter Title",
                Description = "Enter Description",
                Url = "Enter URL",
                Image = "",
            };

            // Get the current set, and append the new record to it because IEnumerable does not have Add
            var dataSet = GetAllData();
            dataSet = dataSet.Append(data);

            SaveData(dataSet);

            return data;
        }

        /// <summary>
        /// Create a new product using product passed
        /// </summary>
        /// <returns>The newly created model</returns>
        public ProductModel CreateNewProduct(ProductModel product)
        {
            var dataSet = GetAllData();
            dataSet = dataSet.Append(product);

            SaveData(dataSet);

            return product;
        }

        /// <summary>
        /// Remove the item from the system
        /// </summary>
        /// <returns>The deleted product if found otherwise null</returns>
        public ProductModel DeleteData(string id)
        {
            // Get the current set, and append the new record to it
            var dataSet = GetAllData();
            var data = dataSet.FirstOrDefault(m => m.Id.Equals(id));

            var newDataSet = GetAllData().Where(m => m.Id.Equals(id) == false);
            
            SaveData(newDataSet);

            return data;
        }
        
    }
}