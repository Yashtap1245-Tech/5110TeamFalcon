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

        [SetUp]
        public void Setup()
        {
        }

        #region GetDataForRead

        [Test]
        public void GetDataForRead_Existing_Product_Should_Return_Product()
        {
            // Arrange
            var productId = "jenlooper-cactus";

            // Act
            var result = TestHelper.ProductService.GetDataForRead(productId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(productId));
        }

        [Test]
        public void GetDataForRead_Non_Existing_Product_Should_Return_Null()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.GetDataForRead("invalid-id");

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
            var result = TestHelper.ProductService.AddRating(null, 1);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_Invalid_Product_Empty_Should_Return_False()
        {
            // Assert

            // Act
            var result = TestHelper.ProductService.AddRating("", 1);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_Invalid_Product_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("invalid-id", 3);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_Invalid_Rating_Below_Zero_Should_Return_False()
        {
            // Arrange
            var productId = "jenlooper-cactus";

            // Act
            var result = TestHelper.ProductService.AddRating(productId, -1);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_Invalid_Rating_Above_Five_Should_Return_False()
        {
            // Arrange
            var productId = "jenlooper-cactus";
            var product = new ProductModel { Id = productId, Ratings = null }; // Set Ratings to null

            // Act
            var result = TestHelper.ProductService.AddRating(productId, 6);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_Valid_Product_Should_Return_True()
        {
            // Arrange
            var productId = "jenlooper-lightshow";

            // Act
            var result = TestHelper.ProductService.AddRating(productId, 5);
            var updatedProduct = TestHelper.ProductService.GetDataForRead(productId);

            // Assert
            Assert.That(result, Is.EqualTo(true));
            Assert.That(updatedProduct.Ratings.Length, Is.EqualTo(2));
            Assert.That(updatedProduct.Ratings.Last(), Is.EqualTo(5));
        }

        #endregion AddRating

        #region UpdateData

        [Test]
        public void UpdateData_Valid_Product_Should_Return_Updated_Product()
        {
            // Arrange
            var productId = "jenlooper-cactus";

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
            var result = TestHelper.ProductService.UpdateData(updatedProduct);
            var retrievedProduct = TestHelper.ProductService.GetDataForRead(productId);

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
            var result = TestHelper.ProductService.UpdateData(nonExistingProduct);

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
            var newProduct = TestHelper.ProductService.CreateData();
            
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
            var productId = "jenlooper-lightshow";

            // Act
            var result = TestHelper.ProductService.DeleteData(productId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(productId));
            Assert.That(TestHelper.ProductService.GetDataForRead(productId), Is.Null); // Verify the product is deleted
        }

        [Test]
        public void DeleteData_Non_Existing_Product_Should_Return_Null()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.DeleteData("invalid-id");

            // Assert
            Assert.That(result, Is.Null); // Ensure that trying to delete a non-existing product returns null
        }

        [Test]
        public void DeleteData_Existing_Product_Multiple_Products_Should_Delete_Correct_Product()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.DeleteData("sailorhg-bubblesortpic");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo("sailorhg-bubblesortpic"));
            Assert.That(TestHelper.ProductService.GetDataForRead("sailorhg-bubblesortpic"), Is.Null); // Verify product 1 is deleted
            Assert.That(TestHelper.ProductService.GetDataForRead("sailorhg-corsage"), Is.Not.Null); // Ensure product 2 still exists
        }

        #endregion DeleteData

        #region GetProductsFromGenre

        [Test]
        public void GetProductsFromGenre_Non_Existing_Genre_Should_Return_Empty_List()
        {
            // Arrange
            var product1 = new ProductModel { Id = "1", Title = "Action Movie 1", Genre = "Action" };
            var product2 = new ProductModel { Id = "2", Title = "Comedy Movie", Genre = "Comedy" };

            // Act
            var result = TestHelper.ProductService.GetProductsFromGenre("Drama");

            // Assert
            Assert.That(result, Is.Empty);
        }

        #endregion GetProductsFromGenre

        #region AddComment

        [Test]
        public void AddComment_Valid_Product_Should_Add_Comment()
        {
            // Arrange
            var productId = "sailorhg-corsage";
            var comment = "This is a great Movie!";
            var result = TestHelper.ProductService.AddComment(productId, comment);

            // Act
            var updatedProduct = TestHelper.ProductService.GetDataForRead(productId);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(updatedProduct.CommentList, Contains.Item(comment));
        }

        [Test]
        public void AddComment_Duplicate_Comment_Should_Return_True_And_Add_Comment()
        {
            // Arrange
            var productId = "sailorhg-bubblesortpic";
            var comment = "This is a great product!";
            var product = new ProductModel { Id = productId, Title = "Test Product", CommentList = new List<string> { comment } };

            // Act
            var initialresult = TestHelper.ProductService.AddComment(productId, comment); // Adding a Initial comment
            var result = TestHelper.ProductService.AddComment(productId, comment); // Adding a duplicate comment
            var updatedProduct = TestHelper.ProductService.GetDataForRead(productId);

            // Assert
            Assert.That(result, Is.True); // Method still returns true
            Assert.That(updatedProduct.CommentList.Count, Is.EqualTo(2)); // The comment list should now have 2 of the same comment
        }

        #endregion AddComment
    }
}
