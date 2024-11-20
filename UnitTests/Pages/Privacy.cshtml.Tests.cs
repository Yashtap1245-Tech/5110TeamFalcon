using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Privacy
{
    /// <summary>
    /// Unit tests for the PrivacyModel class.
    /// </summary>
    public class PrivacyModelTests
    {
        #region TestSetup

        public static PrivacyModel pageModel;

        /// <summary>
        /// Initializes the test setup before each test is executed.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var mockLogger = Mock.Of<ILogger<PrivacyModel>>();

            pageModel = new PrivacyModel(mockLogger)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Test to verify the OnGet method behavior when the activity is set to valid conditions.
        /// Expected result: Model state should be valid.
        /// </summary>
        [Test]
        public void OnGet_Valid_ActivitySet_ModelStateIsValid()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Reset

            // Assert
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
        }

        #endregion OnGet
    }
}
