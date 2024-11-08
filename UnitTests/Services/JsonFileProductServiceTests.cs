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
        public void GetDataForRead_Existing_Product_Should_Return_Product()
        {
            // Arrange
            var productId = "1";
            var product = new ProductModel { Id = productId, Title = "Test Product" };
            CreateTestFile(new List<ProductModel> { product });

            // Act
            var result = _service.GetDataForRead(productId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(productId));
        }

        [Test]
        public void GetDataForRead_Non_Existing_Product_Should_Return_Null()
        {
            // Arrange
            CreateTestFile(new List<ProductModel>());

            // Act
            var result = _service.GetDataForRead("invalid-id");

            // Assert
            Assert.That(result, Is.Null);
        }

        #endregion GetDataForRead

        #region AddRating

        [Test]
        public void AddRating_Invalid_Product_Null_Should_Return_False()
        {
            // Arrange

            // Act
            var result = _service.AddRating(null, 1);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_Invalid_Product_Empty_Should_Return_False()
        {
            // Assert

            // Act
            var result = _service.AddRating("", 1);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_Invalid_Product_Should_Return_False()
        {
            // Arrange
            CreateTestFile(new List<ProductModel>()); // No products exist

            // Act
            var result = _service.AddRating("invalid-id", 3);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_Invalid_Rating_Below_Zero_Should_Return_False()
        {
            // Arrange
            var productId = "1";
            var product = new ProductModel { Id = productId, Ratings = null }; // Set Ratings to null
            CreateTestFile(new List<ProductModel> { product });

            // Act
            var result = _service.AddRating(productId, -1);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_Invalid_Rating_Above_Five_Should_Return_False()
        {
            // Arrange
            var productId = "1";
            var product = new ProductModel { Id = productId, Ratings = null }; // Set Ratings to null
            CreateTestFile(new List<ProductModel> { product });

            // Act
            var result = _service.AddRating(productId, 6);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_Valid_Product_Should_Return_True()
        {
            // Arrange
            var productId = "1";
            var product = new ProductModel { Id = productId, Ratings = null }; // Set Ratings to null
            CreateTestFile(new List<ProductModel> { product });

            // Act
            var result = _service.AddRating(productId, 5);
            var updatedProduct = _service.GetDataForRead(productId);

            // Assert
            Assert.That(result, Is.EqualTo(true));
            Assert.That(updatedProduct.Ratings.Length, Is.EqualTo(1));
            Assert.That(updatedProduct.Ratings.Last(), Is.EqualTo(5));
        }

        #endregion AddRating

        #region UpdateData

        [Test]
        public void UpdateData_Valid_Product_Should_Return_Updated_Product()
        {
            // Arrange
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

            // Act
            var result = _service.UpdateData(updatedProduct);
            var retrievedProduct = _service.GetDataForRead(productId);

            // Assert
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
        public void UpdateData_Non_Existing_Product_Should_Return_Null()
        {
            // Arrange
            var nonExistingProduct = new ProductModel
            {
                Id = "999", // An ID that does not exist
                Title = "Non-Existent Title"
            };

            // Act
            var result = _service.UpdateData(nonExistingProduct);

            // Assert
            Assert.That(result, Is.Null);
        }

        #endregion UpdateData

        #region CreateData

        [Test]
        public void CreateData_Should_Create_New_Product()
        {
            // Arrange

            // Act
            var newProduct = _service.CreateData();
            
            // Assert
            Assert.That(newProduct, Is.Not.Null);
            Assert.That(newProduct.Title, Is.EqualTo("Enter Title"));
        }

        #endregion CreateData

        #region DeleteData

        [Test]
        public void DeleteData_Existing_Product_Should_Delete_Product()
        {
            // Arrange
            var productId = "1";
            var product = new ProductModel { Id = productId, Title = "Test Product" };
            CreateTestFile(new List<ProductModel> { product });

            // Act
            var result = _service.DeleteData(productId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(productId));
            Assert.That(_service.GetDataForRead(productId), Is.Null); // Verify the product is deleted
        }

        [Test]
        public void DeleteData_Non_Existing_Product_Should_Return_Null()
        {
            // Arrange
            CreateTestFile(new List<ProductModel>());

            // Act
            var result = _service.DeleteData("invalid-id");

            // Assert
            Assert.That(result, Is.Null); // Ensure that trying to delete a non-existing product returns null
        }

        [Test]
        public void DeleteData_Existing_Product_Multiple_Products_Should_Delete_Correct_Product()
        {
            // Arrange
            var product1 = new ProductModel { Id = "1", Title = "Product 1" };
            var product2 = new ProductModel { Id = "2", Title = "Product 2" };
            CreateTestFile(new List<ProductModel> { product1, product2 });

            // Act
            var result = _service.DeleteData("1");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo("1"));
            Assert.That(_service.GetDataForRead("1"), Is.Null); // Verify product 1 is deleted
            Assert.That(_service.GetDataForRead("2"), Is.Not.Null); // Ensure product 2 still exists
        }

        #endregion DeleteData

        #region GetProductsFromGenre

        [Test]
        public void GetProductsFromGenre_Non_Existing_Genre_Should_Return_Empty_List()
        {
            // Arrange
            var product1 = new ProductModel { Id = "1", Title = "Action Movie 1", Genre = "Action" };
            var product2 = new ProductModel { Id = "2", Title = "Comedy Movie", Genre = "Comedy" };
            CreateTestFile(new List<ProductModel> { product1, product2 });

            // Act
            var result = _service.GetProductsFromGenre("Drama");

            // Assert
            Assert.That(result, Is.Empty);
        }

        #endregion GetProductsFromGenre

        #region AddComment

        [Test]
        public void AddComment_Valid_Product_Should_Add_Comment()
        {
            // Arrange
            var productId = "1";
            var product = new ProductModel { Id = productId, Title = "Test Product", CommentList = new List<string>() };
            CreateTestFile(new List<ProductModel> { product });
            var comment = "This is a great Movie!";
            var result = _service.AddComment(productId, comment);

            // Act
            var updatedProduct = _service.GetDataForRead(productId);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(updatedProduct.CommentList, Contains.Item(comment));
        }

        [Test]
        public void AddComment_Duplicate_Comment_Should_Return_True_And_Add_Comment()
        {
            // Arrange
            var productId = "1";
            var comment = "This is a great product!";
            var product = new ProductModel { Id = productId, Title = "Test Product", CommentList = new List<string> { comment } };
            CreateTestFile(new List<ProductModel> { product });

            // Act
            var result = _service.AddComment(productId, comment); // Adding a duplicate comment
            var updatedProduct = _service.GetDataForRead(productId);

            // Assert
            Assert.That(result, Is.True); // Method still returns true
            Assert.That(updatedProduct.CommentList.Count, Is.EqualTo(2)); // The comment list should now have 2 of the same comment
        }

        #endregion AddComment

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
