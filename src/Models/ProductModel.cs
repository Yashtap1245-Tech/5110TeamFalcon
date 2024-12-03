using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Attributes of the Product
    /// </summary>
    public class ProductModel
    {
        // ID Field
        public string Id { get; set; }

        // Image Field
        [JsonPropertyName("img")]
        public string Image { get; set; }

        // Title Field
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "The Title should have a length of more than {2} and less than {1}.")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        // Director Field
        [StringLength(maximumLength: 80, MinimumLength = 1, ErrorMessage = "The Movie should have a director.")]
        [Required(ErrorMessage = "Director is required")]
        public string Director { get; set; }

        // Description Field
        [StringLength(maximumLength: 800, MinimumLength = 1, ErrorMessage = "The description should not been more than 800 characters.")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        // Ratings Field
        public int[] Ratings { get; set; }

        // Cast Field
        [Required(ErrorMessage = "Cast is required")]
        [MinLength(1, ErrorMessage = "At least one cast member is required.")]
        public List<string> Cast { get; set; } = new List<string>();

        // Release Year Field
        [Range(1000, 9999, ErrorMessage = "The year must be a four-digit number.")]
        public int ReleaseYear { get; set; }

        // YouTubeID for movie trailer
        [Required(ErrorMessage = "YouTubeID is required")]
        public string YouTubeID { get; set; }

        // Genre field
        [Required(ErrorMessage = "Genre is required")]
        public GenreEnum Genre { get; set; } = GenreEnum.Undefined;
        
        [JsonIgnore]
        public string GenreDisplayName => Genre.DisplayName();

        // IMDb Rating Field
        public float IMDbRating { get; set; }

        // Store the Comments entered by the users on this product
        public List<Comment> CommentList { get; set; } = new List<Comment>();
    }

    public class Comment
    {
        [StringLength(maximumLength: 200, MinimumLength = 3, ErrorMessage = "Should have a length between 3 and 200 characters.")]
        public string Text { get; set; }
        public string SentimentType { get; set; }
        public int SentimentPositive { get; set; }
    }
}