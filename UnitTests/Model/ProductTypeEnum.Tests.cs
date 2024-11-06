namespace UnitTests.Model;

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

}