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

namespace UnitTests.Pages.Product.Create
{
    public class CreateTests
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

        public static CreateModel PageModel;

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

            PageModel = new CreateModel(productService)
            {
            };
        }

        #endregion TestSetup


        #region OnPost Tests

        [Test]
        /// <summary>
        /// Tests the OnPost method when the ModelState is invalid.
        /// Ensures that the method returns a PageResult, allowing the user to correct the errors.
        /// </summary>
        public void OnPost_InvalidModelState_Should_Return_Page()
        {
            // Arrange
            PageModel.ModelState.AddModelError("Product.Name", "Product name is required");

            // Act
            var result = PageModel.OnPost();

            // Assert
            Assert.That(result, Is.TypeOf<PageResult>());
        }

        [Test]
        /// <summary>
        /// Tests the OnPost method when the ModelState is valid.
        /// Ensures that a new product is created, an ID is generated, and the method redirects the user to another page.
        /// </summary>
        public void OnPost_ValidModelState_Should_Create_New_Product_And_Redirect()
        {
            // Arrange
            PageModel.Product = new ProductModel { Title = "New Product" };

            // Act
            var result = PageModel.OnPost();

            // Assert
            Assert.That(result, Is.TypeOf<RedirectToPageResult>());
            Assert.That(!string.IsNullOrEmpty(PageModel.Product.Id), Is.True); // Ensure new ID was generated
        }

        [Test]
        /// <summary>
        /// Tests the OnPost method for a product with an empty ID.
        /// Ensures that a new ID is generated for the product.
        /// </summary>
        public void OnPost_Empty_Product_Id_Should_Generate_New_Id()
        {
            // Arrange
            PageModel.Product = new ProductModel { Title = "Product with Empty Id", Id = "" };

            // Act
            PageModel.OnPost();

            // Assert
            Assert.That(!string.IsNullOrEmpty(PageModel.Product.Id), Is.True); // New ID should be generated
        }

        [Test]
        /// <summary>
        /// Tests the OnPost method for a product with an existing ID.
        /// Ensures that the existing ID remains unchanged.
        /// </summary>
        public void OnPost_Existing_Product_Id_Should_Not_Change_Id()
        {
            // Arrange
            var existingProductId = "12345";
            PageModel.Product = new ProductModel { Title = "Existing Product", Id = existingProductId };

            // Act
            PageModel.OnPost();

            // Assert
            Assert.That(PageModel.Product.Id, Is.EqualTo(existingProductId)); // ID should remain unchanged
        }

        #endregion OnPost Tests
    }
}