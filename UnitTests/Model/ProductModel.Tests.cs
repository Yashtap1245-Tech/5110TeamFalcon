using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UnitTests.Model
{
    [TestFixture]
    public class ProductModelTests
    {
        #region Should_Have_Title_With_Length_Between_1_And_33

        // Test case to validate that Title has a length between 1 and 33 characters
        [Test]
        public void Should_Have_Title_With_Length_Between_1_And_33()
        {
            // Arrange
            var product = new ProductModel
            {
                Title = "Pulp Fiction",
                Description = "Pulp Fiction is a 1994 American independent crime film written and directed by Quentin Tarantino from a story he conceived with Roger Avary. It tells four intertwining tales of crime and violence in Los Angeles, California. The film stars John Travolta, Samuel L. Jackson, Bruce Willis, Tim Roth, Ving Rhames, and Uma Thurman. The title refers to the pulp magazines and hardboiled crime novels popular during the mid-20th century, known for their graphic violence and punchy dialogue.",
                Director = "Quentin Tarantino",
                Cast = new List<string> { "John Travolta", "Samuel Jackson", "Quentin Tarantino" },
                Genre = "Crime",
                YouTubeID = "tGpTpVyI_OQ",
            };

            // Act
            var validationResults = ValidateModel(product);

            // Assert
            Assert.That(validationResults, Is.Empty); // No validation errors should occur
        }

        #endregion Should_Have_Title_With_Length_Between_1_And_33

        #region Title_Should_Fail_Validation_When_Length_Is_Too_Short

        // Test case to validate that Title fails validation when its length is too short (empty string)
        [Test]
        public void Title_Should_Fail_Validation_When_Length_Is_Too_Short()
        {
            // Arrange
            var product = new ProductModel { Title = "" };

            // Act
            var validationResults = ValidateModel(product);

            // Assert
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("Title is required"));
        }

        #endregion Title_Should_Fail_Validation_When_Length_Is_Too_Short

        #region Title_Should_Fail_Validation_When_Length_Exceeds_Maximum

        // Test case to validate that Title fails validation when its length exceeds the maximum allowed
        [Test]
        public void Title_Should_Fail_Validation_When_Length_Exceeds_Maximum()
        {
            // Arrange
            var product = new ProductModel { Title = new string('A', 51) };

            // Act
            var validationResults = ValidateModel(product);

            // Assert
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("The Title should have a length of more than 1 and less than 50"));
        }

        #endregion Title_Should_Fail_Validation_When_Length_Exceeds_Maximum

        #region Helper_Method_ValidateModel

        // Helper method to validate a ProductModel object based on its data annotations
        private static List<ValidationResult> ValidateModel(ProductModel model)
        {
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        #endregion Helper_Method_ValidateModel
    }
}
