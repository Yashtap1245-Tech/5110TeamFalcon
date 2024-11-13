namespace UnitTests.Model
{
    using NUnit.Framework;
    using ContosoCrafts.WebSite.Models;

    [TestFixture]
    public class ProductTypeEnumTests
    {
        [Test]
        public void DisplayName_Should_Return_Correct_Name_For_Collectable()
        {
            // Arrange
            var productType = ProductTypeEnum.Collectable;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("Collectables"));
        }

        [Test]
        public void DisplayName_Should_Return_Correct_Name_For_Commercial()
        {
            // Arrange
            var productType = ProductTypeEnum.Commercial;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("Commercial goods"));
        }

        [Test]
        public void DisplayName_Should_Return_Correct_Name_For_Amature()
        {
            // Arrange
            var productType = ProductTypeEnum.Amature;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("Hand Made Items"));
        }

        [Test]
        public void DisplayName_Should_Return_Correct_Name_For_Antique()
        {
            // Arrange
            var productType = ProductTypeEnum.Antique;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("Antiques"));
        }

        [Test]
        public void DisplayName_Should_Return_Empty_String_For_Undefined()
        {
            // Arrange
            var productType = ProductTypeEnum.Undefined;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo(""));
        }

        [Test]
        public void DisplayName_Should_Return_Empty_String_For_Unknown_Value()
        {
            // Arrange
            var productType = (ProductTypeEnum)999; // Invalid value outside defined enum values.

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo(""));
        }
    }
}
