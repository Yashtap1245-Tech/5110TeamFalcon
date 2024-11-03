using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        // Property to hold the unique request identifier.
        public string RequestId { get; set; }

        // Property that indicates whether to show the RequestId.
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        
        // Logger instance for logging error information.
        private readonly ILogger<ErrorModel> _logger;

        /// <summary>
        /// Constructor initializing the ErrorModel with a logger for logging errors.
        /// </summary>
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// REST OnGet
        /// Return all the data
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}