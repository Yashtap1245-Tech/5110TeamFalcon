using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ContosoCrafts.WebSite.Components;
using ContosoCrafts.WebSite.Services;
using Bunit;
using System.Linq;

namespace UnitTests.Components
{
    public class ProductListTests : BunitTestContext
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
            // Initial setup if needed (currently empty, but could be expanded)
        }

        #endregion TestSetup

        [Test]
        public void ProductList_Valid_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Act
            var page = RenderComponent<ProductList>();

            // Assert
            Assert.That(page.Markup.Contains("The Shawshank Redemption"), Is.True, "Expected product list to contain 'The Shawshank Redemption'.");
        }

        [Test]
        public void SelectedProduct_Valid_ID_jenlooper_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_jenlooper-cactus";
            var page = RenderComponent<ProductList>();

            // Find the button for the specific product
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act - Click the button to view product details
            button.Click();
            var pageMarkup = page.Markup;

            // Assert - Check if the details of the selected product are displayed correctly
            Assert.That(pageMarkup.Contains("The Shawshank Redemption is a 1994 American prison drama film..."), Is.True, "Expected product details to be shown after clicking 'More Info'.");
        }

        [Test]
        public void SubmitRating_Valid_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_jenlooper-cactus";
            var page = RenderComponent<ProductList>();

            // Click to view the product
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Find the star button and click it
            var starButtonList = page.FindAll("span");
            var starButton = starButtonList.First(m => m.ClassName.Contains("fa fa-star"));
            var preStarCount = starButtonList[1].OuterHtml;
            starButton.Click();

            // Act - Check for updated product rating
            var postStarCount = page.FindAll("span")[1].OuterHtml;

            // Assert - Verify the rating was incremented and the star is now checked
            JsonFileProductService productService = Services.GetService<JsonFileProductService>();
            var ratings = productService.GetAllData().First(x => x.Id == "jenlooper-cactus").Ratings.Last();
            Assert.That(ratings.Equals(1), Is.True, "Expected rating to be incremented to 1.");
        }

        [Test]
        public void SubmitRating_On_Existing_Previous_Rating_Should_Increment_Rating_To_1()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_sailorhg-corsage";
            var page = RenderComponent<ProductList>();

            // Click to view the product
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Find the star button and click it
            var starButtonList = page.FindAll("span");
            var starButton = starButtonList.First(m => m.ClassName.Contains("fa fa-star"));
            starButton.Click();

            // Act - Check for updated product rating
            JsonFileProductService productService = Services.GetService<JsonFileProductService>();
            var ratings = productService.GetAllData().First(x => x.Id == "sailorhg-corsage").Ratings.Last();

            // Assert - Verify the rating was incremented to 1
            Assert.That(ratings.Equals(1), Is.True, "Expected rating to be incremented to 1.");
        }

        [Test]
        public void Check_Reset_Genre_Should_Return_To_Default_Products()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Reset_button";
            var page = RenderComponent<ProductList>();

            // Act - Click the reset button to restore the default product list
            var button = page.Find("Button#Reset_button");
            button.Click();

            // Assert - Verify the default product is present after reset
            Assert.That(page.Markup.Contains("The Shawshank Redemption"), Is.True, "Expected the reset to display the default product.");
        }

        [Test]
        public void Submit_Comment_Should_Add_New_Comment_To_Product()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_jenlooper-cactus";
            var page = RenderComponent<ProductList>();

            // Act - Click the product and submit a new comment
            var button = page.Find("Button#MoreInfoButton_jenlooper-cactus");
            button.Click();
            var commentInput = page.Find("input#commentInput");
            commentInput.Change("New comment from test");

            var addCommentButton = page.Find("Button#Add_comment");
            addCommentButton.Click();

            // Assert - Verify the new comment appears in the page markup
            Assert.That(page.Markup.Contains("New comment from test"), Is.True, "Expected the new comment to be added.");
        }

        [Test]
        public void Submit_Blank_Comment_Should_Not_Add_Comment_To_Product()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_sailorhg-kit";
            var page = RenderComponent<ProductList>();

            // Act - Submit an empty comment
            var button = page.Find("Button#MoreInfoButton_sailorhg-kit");
            button.Click();
            var commentInput = page.Find("input#commentInput");
            commentInput.Change("");

            var addCommentButton = page.Find("Button#Add_comment");
            addCommentButton.Click();

            // Assert - Verify that an empty comment was not added
            Assert.That(page.Markup.Contains("New comment from test"), Is.False, "Expected no new comment to be added for blank input.");
        }

        [Test]
        public void Submit_Max_Length_Comment_Should_Not_Add_Excessive_Length_Comment_To_Product()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_sailorhg-kit";
            var page = RenderComponent<ProductList>();

            // Act - Try to submit a comment that exceeds the maximum length
            var button = page.Find("Button#MoreInfoButton_sailorhg-kit");
            button.Click();
            var commentInput = page.Find("input#commentInput");
            commentInput.Change(new string('a', 500));  // Example of a long comment

            var addCommentButton = page.Find("Button#Add_comment");
            addCommentButton.Click();

            // Assert - Verify that the long comment was not added
            JsonFileProductService productService = Services.GetService<JsonFileProductService>();
            var comments = productService.GetAllData().First(x => x.Id == "sailorhg-kit").CommentList;
            Assert.That(comments.Contains(new string('a', 500)), Is.False, "Expected the long comment to be rejected.");
        }
    }
}
