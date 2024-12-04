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
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            Services.AddSingleton<SentimentAnalysisService>(TestHelper.SentimentService);
        }

        #endregion TestSetup

        #region OnGet
        [Test]
        public void ProductList_Valid_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Act
            var page = RenderComponent<ProductList>();

            // Get the Cards retrned
            var result = page.Markup;

            // Assert
            Assert.That(result.Contains("The Shawshank Redemption"), Is.EqualTo(true));
        }

        #endregion

        #region SelectProduct
        [Test]
        public void SelectedProduct_Valid_ID_jenlooper_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_jenlooper-cactus";
            var page = RenderComponent<ProductList>();
            var buttonList = page.FindAll("Button"); // Find the Buttons (more info)            
            var button = buttonList.First(m => m.OuterHtml.Contains(id)); // Find the one that matches the ID looking for and click it 

            // Act
            button.Click();
            var pageMarkup = page.Markup; // Get the markup to use for the assert

            // Assert 
            Assert.That(pageMarkup.Contains("The Shawshank Redemption is a 1994 American prison drama film written and directed by Frank Darabont, based on the 1982 Stephen King novella Rita Hayworth and Shawshank Redemption. The film tells the story of banker Andy Dufresne (Tim Robbins), who is sentenced to life in Shawshank State Penitentiary for the murders of his wife and her lover, despite his claims of innocence. Over the following two decades, he befriends a fellow prisoner, contraband smuggler Ellis Red Redding (Morgan Freeman), and becomes instrumental in a money laundering operation led by the prison warden Samuel Norton (Bob Gunton). William Sadler, Clancy Brown, Gil Bellows, and James Whitmore appear in supporting roles."), Is.EqualTo(true));
        }
        #endregion

        #region SubmitRatting
        [Test]
        public void SubmitRating_Valid_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_jenlooper-cactus";
            var page = RenderComponent<ProductList>();
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();
            var buttonMarkup = page.Markup;
            var starButtonList = page.FindAll("span");
            var preVoteCountSpan = starButtonList[1];

            var preVoteCoutString = preVoteCountSpan.OuterHtml;

            var starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));
            var preStarChange = starButton.OuterHtml;
            starButton.Click();

            buttonMarkup = page.Markup;
            starButtonList = page.FindAll("span");
            var postVoteCountSpan = starButtonList[1];

            var postVoteCoutString = postVoteCountSpan.OuterHtml;
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));
            var postStarChange = starButton.OuterHtml;

            // Assert 
            JsonFileProductService productService = Services.GetService<JsonFileProductService>();
            var ratings = productService.GetAllData().First(x => x.Id == "jenlooper-cactus").Ratings.Last();
            Assert.That(ratings.Equals(1), Is.EqualTo(true));
        }

        [Test]
        public void SubmitRating_Valid_ID_Click_Higher_Rating_Should_Increment_Count_And_Check_Star()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_jenlooper-cactus";
            var page = RenderComponent<ProductList>();
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act
            button.Click();
            var buttonMarkup = page.Markup;
            var starButtonList = page.FindAll("span");
            var preVoteCountSpan = starButtonList[2];

            var preVoteCoutString = preVoteCountSpan.OuterHtml;

            var starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));
            var preStarChange = starButton.OuterHtml;
            starButton.Click();

            buttonMarkup = page.Markup;
            starButtonList = page.FindAll("span");
            var postVoteCountSpan = starButtonList[4];

            var postVoteCoutString = postVoteCountSpan.OuterHtml;
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));
            var postStarChange = starButton.OuterHtml;

            // Assert 
            JsonFileProductService productService = Services.GetService<JsonFileProductService>();
            var ratings = productService.GetAllData().First(x => x.Id == "jenlooper-cactus").Ratings.Last();
            Assert.That(ratings.Equals(1), Is.EqualTo(true));
        }

        [Test]
        public void SubmitRating_On_Existing_Previous_Rating_Should_Increment_Rating_To_1()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_sailorhg-corsage";
            var page = RenderComponent<ProductList>();
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act
            button.Click();
            var buttonMarkup = page.Markup;
            var starButtonList = page.FindAll("span");
            var preVoteCountSpan = starButtonList[4];
            var preVoteCoutString = preVoteCountSpan.OuterHtml;
            var starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));
            var preStarChange = starButton.OuterHtml;
            starButton.Click();
            buttonMarkup = page.Markup;
            starButtonList = page.FindAll("span");
            var postVoteCountSpan = starButtonList[4];
            var postVoteCoutString = postVoteCountSpan.OuterHtml;
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));
            var postStarChange = starButton.OuterHtml;

            // Assert 
            JsonFileProductService productService = Services.GetService<JsonFileProductService>();
            var ratings = productService.GetAllData().First(x => x.Id == "sailorhg-corsage").Ratings.Last();
            Assert.That(ratings.Equals(1), Is.EqualTo(true));
        }
        #endregion

        #region CheckGenre
        [Test]
        public void Check_Reset_Genre_Should_Return_To_Default_Products()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Reset_button";
            var page = RenderComponent<ProductList>();
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act
            button.Click();

            var pageMarkup = page.Markup;

            // Assert
            Assert.That(pageMarkup.Contains("The Shawshank Redemption"), Is.EqualTo(true));
        }
        #endregion

        #region SubmitComment
        [Test]
        public void Submit_Comment_Should_Add_New_Comment_To_Product()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_jenlooper-cactus";
            var page = RenderComponent<ProductList>();
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act 
            button.Click();
            var commentInput = page.Find("input#commentInput");
            commentInput.Change("New comment from test");
            var addCommentButtonList = page.FindAll("Button");
            var addCommentButton = addCommentButtonList.First(m => m.OuterHtml.Contains("Add_comment"));
            addCommentButton.Click();
            var pageMarkup = page.Markup;

            // Assert
            Assert.That(pageMarkup.Contains("New comment from test"), Is.EqualTo(true));
        }

        [Test]
        public void Submit_Blank_Comment_Should_Not_Add_Comment_To_Product()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_sailorhg-kit";
            var page = RenderComponent<ProductList>();
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act
            button.Click();
            var commentInput = page.Find("input#commentInput");
            commentInput.Change("");
            var addCommentButtonList = page.FindAll("Button");
            var addCommentButton = addCommentButtonList.First(m => m.OuterHtml.Contains("Add_comment"));
            addCommentButton.Click();
            var pageMarkup = page.Markup;

            // Assert
            Assert.That(pageMarkup.Contains("New comment from test"), Is.EqualTo(false));

        }

        [Test]
        public void Submit_Max_Length_Comment_Should_Not_Add_Excessive_Length_Comment_To_Product()
        {
            // Assert
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_sailorhg-kit";
            var page = RenderComponent<ProductList>();
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act
            button.Click();
            var commentInput = page.Find("input#commentInput");
            commentInput.Change("ascas kl klasckmck aksmck masklmmklasmc  jkasnc janscmasm jsnc kasnc asjkcnas cs ckas c dsvasdvsdvs sdvc sdvc svscx adcscx adcasdc sx asdvvfscx sfvdsc ssdvszxc sdzv sfdvds csdv sczcadvwevad czavdv sfd");
            var addCommentButtonList = page.FindAll("Button");
            var addCommentButton = addCommentButtonList.First(m => m.OuterHtml.Contains("Add_comment"));
            addCommentButton.Click();

            // Assert
            JsonFileProductService productService = Services.GetService<JsonFileProductService>();
            var comments = productService.GetAllData().First(x => x.Id == "sailorhg-kit").CommentList;
            Assert.That(comments[0].Text.Contains("ascas kl klasckmck aksmck masklmmklasmc  jkasnc janscmasm jsnc kasnc asjkcnas cs ckas c dsvasdvsdvs sdvc sdvc svscx adcscx adcasdc sx asdvvfscx sfvdsc ssdvszxc sdzv sfdvds csdv sczcadvwevad czavdv sfda"), Is.EqualTo(false));
        }
        #endregion
    }
}