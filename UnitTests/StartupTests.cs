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

        public class Startup : ContosoCrafts.WebSite.Startup
        {
            public Startup(IConfiguration config) : base(config) { }
        }
        #endregion TestSetup

        #region ConfigureServices
        [Test]
        public void Startup_ConfigureServices_Valid_Defaut_Should_Pass()
        {
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Assert.That(webHost, Is.Not.Null);
        }
        #endregion ConfigureServices

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