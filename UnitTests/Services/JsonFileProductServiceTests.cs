using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace UnitTests.Services
{
    [TestFixture]
    public class JsonFileProductServiceTests
    {
        private JsonFileProductService _service;
        private TestWebHostEnvironment _webHostEnvironment;

        [SetUp]
        public void Setup()
        {
            _webHostEnvironment = new TestWebHostEnvironment();
            _service = new JsonFileProductService(_webHostEnvironment);
        }

        #region GetDataForRead

        [Test]
        public void GetDataForRead_ExistingProduct_ReturnsProduct()
        {
            var productId = "1";
            var product = new ProductModel { Id = productId, Title = "Test Product" };
            CreateTestFile(new List<ProductModel> { product });

            var result = _service.GetDataForRead(productId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(productId));
        }

        [Test]
        public void GetDataForRead_NonExistingProduct_ReturnsNull()
        {
            CreateTestFile(new List<ProductModel>());

            var result = _service.GetDataForRead("invalid-id");

            Assert.That(result, Is.Null);
        }

        #endregion GetDataForRead

        #region AddRating

        [Test]
        public void AddRating_InValid_Product_Null_Should_Return_False()
        {
            var result = _service.AddRating(null, 1);
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_InValid_Product_Empty_Should_Return_False()
        {
            var result = _service.AddRating("", 1);
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_Invalid_Product_Should_Return_False()
        {
            CreateTestFile(new List<ProductModel>()); // No products exist
            var result = _service.AddRating("invalid-id", 3);
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_Invalid_Rating_Below_0_Should_Return_False()
        {
            var productId = "1";
            var product = new ProductModel { Id = productId, Ratings = null }; // Set Ratings to null
            CreateTestFile(new List<ProductModel> { product });

            var result = _service.AddRating(productId, -1);
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_Invalid_Rating_Above_5_Should_Return_False()
        {
            var productId = "1";
            var product = new ProductModel { Id = productId, Ratings = null }; // Set Ratings to null
            CreateTestFile(new List<ProductModel> { product });

            var result = _service.AddRating(productId, 6);
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_Valid_Product_Rating_Should_Return_True()
        {
            var productId = "1";
            var product = new ProductModel { Id = productId, Ratings = null }; // Set Ratings to null
            CreateTestFile(new List<ProductModel> { product });

            var result = _service.AddRating(productId, 5);
            var updatedProduct = _service.GetDataForRead(productId);

            Assert.That(result, Is.EqualTo(true));
            Assert.That(updatedProduct.Ratings.Length, Is.EqualTo(1));
            Assert.That(updatedProduct.Ratings.Last(), Is.EqualTo(5));
        }

        #endregion AddRating

        #region UpdateData

        [Test]
        public void UpdateData_ValidProduct_Should_ReturnUpdatedProduct()
        {
            var productId = "1";
            var originalProduct = new ProductModel
            {
                Id = productId,
                Title = "Original Title",
                Image = "original.png",
                Description = "Original Description",
                Genre = "Original Genre",
                YouTubeID = "OriginalYouTubeID",
                Director = "Original Director",
                Cast = new List<string> { "Original Cast" }
            };

            CreateTestFile(new List<ProductModel> { originalProduct });

            var updatedProduct = new ProductModel
            {
                Id = productId,
                Title = "Updated Title",
                Image = "updated.png",
                Description = "Updated Description",
                Genre = "Updated Genre",
                YouTubeID = "UpdatedYouTubeID",
                Director = "Updated Director",
                Cast = new List<string> { "Updated Cast" }
            };

            var result = _service.UpdateData(updatedProduct);
            var retrievedProduct = _service.GetDataForRead(productId);

            Assert.That(result, Is.Not.Null);
            Assert.That(retrievedProduct.Title, Is.EqualTo("Updated Title"));
            Assert.That(retrievedProduct.Image, Is.EqualTo("updated.png"));
            Assert.That(retrievedProduct.Description, Is.EqualTo("Updated Description"));
            Assert.That(retrievedProduct.Genre, Is.EqualTo("Updated Genre"));
            Assert.That(retrievedProduct.YouTubeID, Is.EqualTo("UpdatedYouTubeID"));
            Assert.That(retrievedProduct.Director, Is.EqualTo("Updated Director"));
            Assert.That(retrievedProduct.Cast, Is.EqualTo(new List<string> { "Updated Cast" }));
        }

        [Test]
        public void UpdateData_NonExistingProduct_Should_ReturnNull()
        {
            var nonExistingProduct = new ProductModel
            {
                Id = "999", // An ID that does not exist
                Title = "Non-Existent Title"
            };

            var result = _service.UpdateData(nonExistingProduct);
            Assert.That(result, Is.Null);
        }

        #endregion UpdateData

        #region CreateData

        [Test]
        public void CreateData_CreatesNewProduct()
        {
            var newProduct = _service.CreateData();
            Assert.That(newProduct, Is.Not.Null);
            Assert.That(newProduct.Title, Is.EqualTo("Enter Title"));
        }

        #endregion CreateData

        #region DeleteData

        [Test]
        public void DeleteData_ExistingProduct_DeletesProduct()
        {
            var productId = "1";
            var product = new ProductModel { Id = productId, Title = "Test Product" };
            CreateTestFile(new List<ProductModel> { product });

            var result = _service.DeleteData(productId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(productId));
            Assert.That(_service.GetDataForRead(productId), Is.Null); // Verify the product is deleted
        }

        [Test]
        public void DeleteData_NonExistingProduct_ReturnsNull()
        {
            CreateTestFile(new List<ProductModel>());
            var result = _service.DeleteData("invalid-id");
            Assert.That(result, Is.Null); // Ensure that trying to delete a non-existing product returns null
        }

        [Test]
        public void DeleteData_DeletesCorrectProduct_WhenMultipleProductsExist()
        {
            var product1 = new ProductModel { Id = "1", Title = "Product 1" };
            var product2 = new ProductModel { Id = "2", Title = "Product 2" };
            CreateTestFile(new List<ProductModel> { product1, product2 });

            var result = _service.DeleteData("1");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo("1"));
            Assert.That(_service.GetDataForRead("1"), Is.Null); // Verify product 1 is deleted
            Assert.That(_service.GetDataForRead("2"), Is.Not.Null); // Ensure product 2 still exists
        }

        #endregion DeleteData

        private void CreateTestFile(IEnumerable<ProductModel> products)
        {
            var json = JsonSerializer.Serialize(products);
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "data", "products.json");
            File.WriteAllText(filePath, json);
        }

        private class TestWebHostEnvironment : IWebHostEnvironment
        {
            public string ApplicationName { get; set; } = "TestApplication";
            public string EnvironmentName { get; set; } = "Development";
            public string ContentRootPath { get; set; } = Path.GetTempPath();
            public string WebRootPath => Path.Combine(Path.GetTempPath(), "wwwroot");

            public TestWebHostEnvironment()
            {
                Directory.CreateDirectory(WebRootPath + "/data");
            }

            public IFileProvider FileProvider => null;

            string IWebHostEnvironment.WebRootPath
            {
                get => WebRootPath;
                set => throw new System.NotImplementedException();
            }

            IFileProvider IWebHostEnvironment.WebRootFileProvider
            {
                get => null;
                set => throw new System.NotImplementedException();
            }

            IFileProvider IHostEnvironment.ContentRootFileProvider
            {
                get => null;
                set => throw new System.NotImplementedException();
            }
        }
    }
}
