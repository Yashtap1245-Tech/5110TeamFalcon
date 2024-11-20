using System.Diagnostics;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Error
{
    /// <summary>
    /// Unit tests for the ErrorModel class.
    /// </summary>
    public class ErrorTests
    {
        #region TestSetup
        public static ErrorModel pageModel;

        /// <summary>
        /// Initializes the test setup before each test is executed.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<ErrorModel>>();

            pageModel = new ErrorModel(MockLoggerDirect)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Test to verify OnGet method behavior when the activity is valid.
        /// Expected result: RequestId should be the activity ID.
        /// </summary>
        [Test]
        public void OnGet_Valid_ActivitySet_RequestIdIsActivityId()
        {
            // Arrange

            Activity activity = new Activity("activity");
            activity.Start();

            // Act
            pageModel.OnGet();

            // Reset
            activity.Stop();

            // Assert
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(pageModel.RequestId, Is.EqualTo(activity.Id));
        }

        /// <summary>
        /// Test to verify OnGet method behavior when the activity is null.
        /// Expected result: RequestId should be the default value "trace".
        /// </summary>
        [Test]
        public void OnGet_Invalid_ActivityNull_RequestIdIsTraceIdentifier()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Assert
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(pageModel.RequestId, Is.EqualTo("trace"));
            Assert.That(pageModel.ShowRequestId, Is.EqualTo(true));
        }
        #endregion OnGet
    }
}