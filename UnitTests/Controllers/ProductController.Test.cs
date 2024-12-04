using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Controllers;
using ContosoCrafts.WebSite.Services;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class ProductsController_Test
    {
        private ProductsController _controller;
        private JsonFileProductService _productService;
        private string _testWebRootPath;

        // Setup method to initialize test environment
        [SetUp]
        public void Setup()
        {
            // Create a temporary directory to act as WebRootPath
            _testWebRootPath = Path.Combine(Path.GetTempPath(), "TestWebRoot");
            Directory.CreateDirectory(_testWebRootPath);

            // Create a data directory within WebRoot
            string dataDirectory = Path.Combine(_testWebRootPath, "data");
            Directory.CreateDirectory(dataDirectory);

            // Create a test products.json file with sample data
            string productsJsonPath = Path.Combine(dataDirectory, "products.json");
            File.WriteAllText(productsJsonPath, @"
            [
                {
                    ""Id"": ""1"",
                    ""Title"": ""Product 1"",
                    ""Description"": ""Description of Product 1"",
                    ""Url"": ""https://example.com/product1"",
                    ""Image"": ""product1.jpg"",
                    ""Trophys"": 10,
                    ""FoundingYear"": 1990,
                    ""Ratings"": [4, 5]
                },
                {
                    ""Id"": ""2"",
                    ""Title"": ""Product 2"",
                    ""Description"": ""Description of Product 2"",
                    ""Url"": ""https://example.com/product2"",
                    ""Image"": ""product2.jpg"",
                    ""Trophys"": 20,
                    ""FoundingYear"": 1970,
                    ""Ratings"": [3, 4]
                }
            ]");

            // Mock IWebHostEnvironment to return the test WebRootPath
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment.Setup(m => m.WebRootPath).Returns(_testWebRootPath);

            // Initialize the Product Service with the mock environment
            _productService = new JsonFileProductService(mockEnvironment.Object);

            // Initialize the controller with the product service
            _controller = new ProductsController(_productService);
        }

        // Clean up after the tests
        [TearDown]
        public void TearDown()
        {
            // Remove the test directory after tests
            if (Directory.Exists(_testWebRootPath))
            {
                Directory.Delete(_testWebRootPath, true);
            }
        }

        /// <summary>
        /// Tests the constructor of the ProductsController to ensure that the ProductService
        /// is properly initialized with the provided service instance.
        /// </summary>
        #region ConsturctorInitialze
        [Test]
        public void Constructor_Should_Initialize_ProductService()
        {
            // Assert that the ProductService was correctly injected into the controller
            Assert.That(_controller.ProductService, Is.EqualTo(_productService), "Expected ProductService to be initialized with the provided service.");
        }
        #endregion

        /// <summary>
        /// Tests the Get method of the ProductsController to ensure that it returns all products.
        /// This test verifies that the Get method retrieves the correct number of products from the data store.
        /// </summary>
        #region ReturnProducts
        [Test]
        public void Get_Should_Return_All_Products()
        {
            // Act - Call the Get method
            var result = _controller.Get();

            // Assert - Verify the result is the expected number of products (2 in this case)
            Assert.That(result.Count(), Is.EqualTo(2), "Expected Get() to return two products.");
        }
        #endregion

        /// <summary>
        /// Tests the Patch method of the ProductsController to ensure that it adds a rating to the specified product
        /// and returns an Ok result. This test verifies that the rating is correctly added to the product's Ratings array.
        /// </summary>

        #region PatchRating
        [Test]
        public void Patch_Should_Add_Rating_To_Product_And_Return_Ok()
        {
            // Arrange
            
            var request = new ProductsController.RatingRequest // Create a RatingRequest object with ProductId "1" and Rating 5
            {
                ProductId = "1",
                Rating = 5
            };

            // Act 

            var result = _controller.Patch(request); // Call the Patch method with the request

            // Assert

            // Check that the result is of type OkResult
            Assert.That(result, Is.TypeOf<OkResult>(), "Expected Patch() to return Ok result.");

            // Verify that the rating was successfully added
            var products = _productService.GetAllData();
            var product = products.FirstOrDefault(p => p.Id == "1");

            // Check if the product with Id "1" exists
            Assert.That(product.Id, Is.EqualTo("1"));

            // Check if the Ratings array is not empty
            Assert.That(product.Ratings.Length > 0, Is.EqualTo(true));

            // Check if the Ratings array contains the new rating (5)
            Assert.That(product.Ratings, Contains.Item(5), "Product Ratings should contain the new rating.");
        }
        #endregion
    }
}