using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    public class PrivacyModel : PageModel
    {
        // Logger instance for logging information related to the Privacy page.
        private readonly ILogger<PrivacyModel> _logger;
        
        /// <summary>
        /// Constructor initializing the PrivacyModel with a logger for logging purposes.
        /// </summary>
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// REST OnGet
        /// Return all the data
        /// </summary>
        public void OnGet()
        {
        }
    }
}
