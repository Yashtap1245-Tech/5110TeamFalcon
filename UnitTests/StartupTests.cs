using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

using NUnit.Framework;

namespace UnitTests.Pages.Startup
{
    /// <summary>
    /// Unit tests for the Startup class of the application.
    /// Verifies the configuration and initialization of the application during startup.
    /// </summary>
    public class StartupTests
    {
        #region TestSetup
        /// <summary>
        /// Setup method for initializing test dependencies.
        /// Runs before each test in this class.
        /// </summary>

        [SetUp]
        public void TestInitialize()
        {
        }

        /// <summary>
        /// Custom implementation of the Startup class for testing purposes.
        /// Inherits from the main Startup class and passes the configuration to the base class.
        /// </summary>
        public class Startup : ContosoCrafts.WebSite.Startup
        {
            public Startup(IConfiguration config) : base(config) { }
        }
        #endregion TestSetup

        #region ConfigureServices

        /// <summary>
        /// Tests the ConfigureServices method of the Startup class.
        /// Verifies that the web host is created successfully with the default configuration and services.
        /// </summary>
        [Test]
        public void Startup_ConfigureServices_Valid_Defaut_Should_Pass()
        {
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Assert.That(webHost, Is.Not.Null);
        }
        #endregion ConfigureServices

        /// <summary>
        /// Tests the Configure method of the Startup class.
        /// Verifies that the web host is initialized successfully with the default settings.
        /// </summary>
        #region Configure
        [Test]
        public void Startup_Configure_Valid_Defaut_Should_Pass()
        {
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Assert.That(webHost, Is.Not.Null);
        }

        #endregion Configure
    }
}