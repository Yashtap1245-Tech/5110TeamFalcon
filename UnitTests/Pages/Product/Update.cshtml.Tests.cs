using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Pages.Product;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace UnitTests.Pages.Product
{
    public class UpdateTests
    {
        #region TestSetup

        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        public static UpdateModel PageModel;

        [SetUp]
        public void TestInitialize()
        {
            httpContextDefault = new DefaultHttpContext()
            {
                //RequestServices = serviceProviderMock.Object,
            };

            modelState = new ModelStateDictionary();

            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
            };

            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            PageModel = new UpdateModel(productService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Tests the OnGet method with a valid product ID.
        /// Ensures that:
        /// 1. The ModelState is valid after invoking the method.
        /// 2. The returned Product object contains the correct details for the specified product ID, including title,
        /// director, description, and genre.
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Product()
        {
            // Arrange
            
            // Act
            PageModel.OnGet("jenlooper-cactus");
            
            Assert.That(PageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(PageModel.Product.Title, Is.EqualTo("The Shawshank Redemption"));
            Assert.That(PageModel.Product.Director, Is.EqualTo("Frank Darabont"));
            Assert.That(PageModel.Product.Description, Is.EqualTo("The Shawshank Redemption is a 1994 American prison drama film written and directed by Frank Darabont, based on the 1982 Stephen King novella Rita Hayworth and Shawshank Redemption. The film tells the story of banker Andy Dufresne (Tim Robbins), who is sentenced to life in Shawshank State Penitentiary for the murders of his wife and her lover, despite his claims of innocence. Over the following two decades, he befriends a fellow prisoner, contraband smuggler Ellis Red Redding (Morgan Freeman), and becomes instrumental in a money laundering operation led by the prison warden Samuel Norton (Bob Gunton). William Sadler, Clancy Brown, Gil Bellows, and James Whitmore appear in supporting roles."));
            Assert.That(PageModel.Product.Genre, Is.EqualTo(GenreEnum.Drama));
        }

        /// <summary>
        /// Tests the OnGet method with an invalid product ID.
        /// Ensures that the method redirects the user to a different page when the specified product ID does not exist.
        /// </summary>
        [Test]
        public void OnGet_Invalid_Id_Should_Redirect_Page()
        {
            // Arrange
            var invalid_Id = "jenlooper-cactusasdasd";
            // Act
            var result = PageModel.OnGet(invalid_Id);

            //Assert
            Assert.That(result, Is.TypeOf<RedirectToPageResult>());
        }

        /// <summary>
        /// Tests the OnPost method when the ModelState is invalid.
        /// Ensures that the ModelState is marked as invalid and that the method does not proceed further.
        /// </summary>
        [Test]
        public void OnPost_InValid_Model_NotValid_Return_Page()
        {
            // Arrange
            // Force an invalid error state
            PageModel.ModelState.AddModelError("bogus", "Bogus Error");
            
            // Act
            var result = PageModel.OnPost() as ActionResult;
            
            // Assert
            Assert.That(PageModel.ModelState.IsValid, Is.EqualTo(false));
        }

        /// <summary>
        /// Tests the OnPost method with a valid product model.
        /// Ensures that:
        /// 1. The ModelState is valid after the method is invoked.
        /// 2. The method redirects to the "Index" page after successfully handling the valid product.
        /// </summary>
        [Test]
        public void OnPost_Valid_Should_Return_Products()
        {
            // Arrange
            PageModel.Product = new ProductModel
            {
                Id = "jenlooper-cactus",
                Title = "The Shawshank Redemption",
                Description =
                    "The Shawshank Redemption is a 1994 American prison drama film written and directed by Frank Darabont, based on the 1982 Stephen King novella Rita Hayworth and Shawshank Redemption. The film tells the story of banker Andy Dufresne (Tim Robbins), who is sentenced to life in Shawshank State Penitentiary for the murders of his wife and her lover, despite his claims of innocence. Over the following two decades, he befriends a fellow prisoner, contraband smuggler Ellis Red Redding (Morgan Freeman), and becomes instrumental in a money laundering operation led by the prison warden Samuel Norton (Bob Gunton). William Sadler, Clancy Brown, Gil Bellows, and James Whitmore appear in supporting roles.",
                Director = "Frank Darabont",
                Image = "The Shawshank Redemption",
                Genre = GenreEnum.Drama,
                YouTubeID = "NmzuHjWmXOc",
            };
            
            // Act
            var result = PageModel.OnPost() as RedirectToPageResult;
            
            // Assert
            Assert.That(PageModel.ModelState.IsValid, Is.EqualTo(true));            
            Assert.That(result.PageName.Contains("Index"), Is.EqualTo(true));            

        }

        #endregion
    }
}