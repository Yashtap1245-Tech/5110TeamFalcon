using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UnitTests.Model
{
    [TestFixture]
    public class ProductModelTests
    {
        #region Cast_Should_Fail_Validation_When_Empty

        [Test]
        public void Cast_Should_Fail_Validation_When_Empty()
        {
            var product = new ProductModel
            {
                Title = "Inception",
                Description = "A mind-bending thriller directed by Christopher Nolan.",
                Director = "Christopher Nolan",
                Cast = new List<string>(),  // Empty Cast list (this should trigger the validation error)
                Genre = GenreEnum.SciFi,
                YouTubeID = "YoHD9XEInc0",
                ReleaseYear = 2010
            };

            var validationResults = ValidateModel(product);
            Assert.That(validationResults, Is.Not.Empty);  // Validation should fail
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("At least one cast member is required."));
        }

        [Test]
        public void GenreDisplayName_Should_Return_Correct_DisplayName()
        {
            // Arrange
            var product = new ProductModel
            {
                Genre = GenreEnum.SciFi
            };

            // Act
            var displayName = product.GenreDisplayName;

            // Assert
            Assert.That(displayName, Is.EqualTo("SciFi"));
        }
        #endregion

        #region Helper_Method_ValidateModel

        // Helper method to validate a ProductModel object based on its data annotations
        private static List<ValidationResult> ValidateModel(ProductModel model)
        {
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        #endregion
    }
}
