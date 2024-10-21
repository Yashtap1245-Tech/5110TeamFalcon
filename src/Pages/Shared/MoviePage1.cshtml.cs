using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.Shared
{
    public class MoviePage1Model : PageModel
    {
        public string MovieTitle { get; set; }
        public string MoviePosterUrl { get; set; }
        public string Description { get; set; }
        public string CaseDetails { get; set; }
        public double Rating { get; set; } // Use float or decimal if preferred

        public void OnGet()
        {
            MovieTitle = "Inception";
            MoviePosterUrl = "https://example.com/inception.jpg";
            Description = "A thief who steals corporate secrets through the use of dream-sharing technology.";
            CaseDetails = "Director: Christopher Nolan, Release Year: 2010";
            Rating = 8.8;
        }
    }
}
