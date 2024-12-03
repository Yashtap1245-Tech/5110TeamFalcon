namespace UnitTests.Model
{
    using NUnit.Framework;
    using ContosoCrafts.WebSite.Models;

    [TestFixture]
    public class GenreEnumTests
    {
        #region DisplayName_Should_Return_Correct_Name_For_Collectable

        [Test]
        public void DisplayName_Should_Return_Correct_Name_For_Collectable()
        {
            // Arrange
            var productType = GenreEnum.Action;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("Action"));
        }

        #endregion DisplayName_Should_Return_Correct_Name_For_Collectable

        #region DisplayName_Should_Return_Correct_Name_For_Commercial

        [Test]
        public void DisplayName_Should_Return_Correct_Name_For_Commercial()
        {
            // Arrange
            var productType = GenreEnum.Action;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("Action"));
        }

        #endregion DisplayName_Should_Return_Correct_Name_For_Commercial

        #region DisplayName_Should_Return_Correct_Name_For_Amature

        [Test]
        public void DisplayName_Should_Return_Correct_Name_For_Amature()
        {
            // Arrange
            var productType = GenreEnum.Crime;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("Crime"));
        }

        #endregion DisplayName_Should_Return_Correct_Name_For_Amature

        #region DisplayName_Should_Return_Correct_Name_For_Antique

        [Test]
        public void DisplayName_Should_Return_Correct_Name_For_Antique()
        {
            // Arrange
            var productType = GenreEnum.Drama;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("Drama"));
        }

        #endregion DisplayName_Should_Return_Correct_Name_For_Antique

        #region DisplayName_Should_Return_Empty_String_For_Undefined

        [Test]
        public void DisplayName_Should_Return_Empty_String_For_Undefined()
        {
            // Arrange
            var productType = GenreEnum.Undefined;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo(""));  // Undefined should return empty string
        }

        #endregion DisplayName_Should_Return_Empty_String_For_Undefined

        #region DisplayName_Should_Return_Empty_String_For_Unknown_Value

        [Test]
        public void DisplayName_Should_Return_Empty_String_For_Unknown_Value()
        {
            // Arrange
            var productType = (GenreEnum)999;  // Invalid value outside defined enum values

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo(""));  // Invalid enum values should return empty string
        }

        #endregion DisplayName_Should_Return_Empty_String_For_Unknown_Value
    }
}
