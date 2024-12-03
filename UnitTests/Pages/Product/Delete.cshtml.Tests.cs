using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Pages.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace UnitTests.Pages.Product.Delete
{
    public class DeleteTests
    {
        #region TestSetup

        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        public static DeleteModel PageModel;
        private static JsonFileProductService productService;

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

            PageModel = new DeleteModel(productService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet Tests

        [Test]
        public void OnGet_Product_NotFound_Should_Return_NotFound()
        {
            // Arrange
            var nonExistentId = "non-existent-id";

            // Act
            PageModel.OnGet(nonExistentId);

            // Assert
            Assert.That(PageModel.Product, Is.Null);
        }

        [Test]
        public void OnGet_Valid_Should_Return_Valid_Product()
        {
            // Arrange

            // Act
            PageModel.OnGet("jenlooper-cactus");

            // Assert
            Assert.That(PageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(PageModel.Product.Title, Is.EqualTo("The Shawshank Redemption"));
            Assert.That(PageModel.Product.Director, Is.EqualTo("Frank Darabont"));
            Assert.That(PageModel.Product.Description, Is.EqualTo("The Shawshank Redemption is a 1994 American prison drama film written and directed by Frank Darabont, based on the 1982 Stephen King novella Rita Hayworth and Shawshank Redemption. The film tells the story of banker Andy Dufresne (Tim Robbins), who is sentenced to life in Shawshank State Penitentiary for the murders of his wife and her lover, despite his claims of innocence. Over the following two decades, he befriends a fellow prisoner, contraband smuggler Ellis Red Redding (Morgan Freeman), and becomes instrumental in a money laundering operation led by the prison warden Samuel Norton (Bob Gunton). William Sadler, Clancy Brown, Gil Bellows, and James Whitmore appear in supporting roles."));
            Assert.That(PageModel.Product.Genre, Is.EqualTo(GenreEnum.Drama));
        }

        #endregion OnGet Tests

        #region OnPost Tests

        [Test]
        public void OnPost_InvalidModelState_Should_Return_Page()
        {
            // Arrange
            PageModel.ModelState.AddModelError("Product.Id", "Product ID is required");

            // Act
            var result = PageModel.OnPost();

            // Assert
            Assert.That(result, Is.TypeOf<PageResult>());
        }

        [Test]
        public void OnPost_Product_NotFound_Should_Not_Delete_And_Return_Page()
        {
            // Arrange
            var nonExistentProductId = "non-existent-id";
            PageModel.Product = new ProductModel { Id = nonExistentProductId };

            // Act
            var result = PageModel.OnPost();

            // Assert
            Assert.That(result, Is.TypeOf<RedirectToPageResult>());
            Assert.That(PageModel.Product, Is.Not.Null); // Product should not be deleted
        }

        #endregion OnPost Tests
    }
}