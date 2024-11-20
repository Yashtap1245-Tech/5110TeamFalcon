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
        /// Initializes a new instance of the class.
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
        /// <returns>An enumerable collection representing all products.</returns>
        public IEnumerable<ProductModel> GetAllData()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<ProductModel[]>(
                    jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
            }
        }

        /// <summary>
        /// Retrieves a specific product's data for reading, based on the product ID.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>The model which represents the product.</returns>
        public ProductModel GetDataForRead(string id)
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<ProductModel[]>(
                    jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                ).FirstOrDefault(m => m.Id == id);
            }
        }

        /// <summary>
        /// Adds a rating to a specific product.
        /// If the rating does not already exist, it adds the rating and saves the updated product data.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to rate.</param>
        /// <param name="rating">The rating value to add (must be between 0 and 5).</param>
        /// <returns>True if the rating is successfully added, otherwise false.</returns>
        public bool AddRating(string productId, int rating)
        {
            // Fast fail if the product ID is invalid.
            if (string.IsNullOrEmpty(productId)) return false;

            var products = GetAllData();
            var data = products.FirstOrDefault(x => x.Id.Equals(productId));

            // Return false if the product is not found.
            if (data == null) return false;

            // Check Rating for boundaries.
            if (rating < 0 || rating > 5) return false;

            // Initialize Ratings array if it's null.
            data.Ratings ??= new int[] { };

            // Add the rating to the list.
            var ratings = data.Ratings.ToList();
            ratings.Add(rating);
            data.Ratings = ratings.ToArray();

            // Save the data back to the data store.
            SaveData(products);

            return true;
        }

        /// <summary>
        /// Updates the fields of an existing product.
        /// </summary>
        /// <param name="data">The model containing the updated product data.</param>
        /// <returns>The updated model if successful, otherwise null.</returns>
        public ProductModel UpdateData(ProductModel data)
        {
            var products = GetAllData();
            var productData = products.FirstOrDefault(x => x.Id.Equals(data.Id));

            // Return null if the product is not found.
            if (productData == null) return null;

            // Update the product data.
            productData.Title = data.Title;
            productData.Image = data.Image;
            productData.Description = data.Description.Trim();
            productData.Genre = data.Genre;
            productData.YouTubeID = data.YouTubeID;
            productData.Director = data.Director;
            productData.Cast = data.Cast;

            // Save the updated data.
            SaveData(products);

            return productData;
        }

        /// <summary>
        /// Saves all product data to storage.
        /// </summary>
        /// <param name="products">The collection of products to save.</param>
        private void SaveData(IEnumerable<ProductModel> products)
        {
            using (var outputStream = File.Create(JsonFileName))
            {
                JsonSerializer.Serialize(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions { SkipValidation = true, Indented = true }),
                    products
                );
            }
        }

        /// <summary>
        /// Creates a new product with default values.
        /// After creation, the user can update the product to set custom values.
        /// </summary>
        /// <returns>The newly created product model.</returns>
        public ProductModel CreateData()
        {
            var data = new ProductModel
            {
                Id = System.Guid.NewGuid().ToString(),
                Title = "Enter Title",
                Description = "Enter Description",
                Url = "Enter URL",
                Image = string.Empty
            };

            // Get the current set of products and append the new product.
            var dataSet = GetAllData().Append(data);
            SaveData(dataSet);

            return data;
        }

        /// <summary>
        /// Creates a new product using the provided product model.
        /// </summary>
        /// <param name="product">The product model to create.</param>
        /// <returns>The newly created product model.</returns>
        public ProductModel CreateNewProduct(ProductModel product)
        {
            var dataSet = GetAllData().Append(product);
            SaveData(dataSet);

            return product;
        }

        /// <summary>
        /// Removes a product from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        /// <returns>The deleted product if found, otherwise null.</returns>
        public ProductModel DeleteData(string id)
        {
            // Get the current set and remove the product.
            var dataSet = GetAllData();
            var data = dataSet.FirstOrDefault(m => m.Id.Equals(id));

            var newDataSet = dataSet.Where(m => !m.Id.Equals(id));

            SaveData(newDataSet);

            return data;
        }

        /// <summary>
        /// Retrieves unique product genres.
        /// </summary>
        /// <returns>An enumerable collection of unique genres.</returns>
        public IEnumerable<string> GetUniqueGenres()
        {
            return GetAllData()
                   .Select(product => product.Genre)
                   .Distinct()
                   .OrderBy(genre => genre);
        }

        /// <summary>
        /// Retrieves products from a specific genre.
        /// </summary>
        /// <param name="genre">The genre to filter products by.</param>
        /// <returns>An enumerable collection of products from the specified genre.</returns>
        public IEnumerable<ProductModel> GetProductsFromGenre(string genre)
        {
            var dataSet = GetAllData();

            // Return all products if no genre is specified.
            if (string.IsNullOrEmpty(genre)) return dataSet;

            return dataSet.Where(product => product.Genre != null && product.Genre.Equals(genre));
        }

        /// <summary>
        /// Adds a comment to a specific product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="comment">The comment to add.</param>
        /// <returns>True if the comment is successfully added.</returns>
        public bool AddComment(string productId, string comment)
        {
            var products = GetAllData().ToList();
            var data = products.FirstOrDefault(p => p.Id == productId);

            if (data == null) return false;

            var comments = data.CommentList.ToList();
            comments.Add(comment);
            data.CommentList = comments;

            // Save the updated data.
            SaveData(products);

            return true;
        }
    }
}
