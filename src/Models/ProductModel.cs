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
        [StringLength(maximumLength: 500, MinimumLength = 1, ErrorMessage = "The description should not been more than 500 characters.")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        // Ratings Field
        public int[] Ratings { get; set; }

        // Cast Field
        [Required(ErrorMessage = "Cast is required")]
        [MinLength(1, ErrorMessage = "At least one cast member is required.")]
        public List<string> Cast { get; set; } = new List<string>();

        // Product Type
        public ProductTypeEnum ProductType { get; set; } = ProductTypeEnum.Undefined;

        // Release Year Field
        [Range(1000, 9999, ErrorMessage = "The year must be a four-digit number.")]
        public int ReleaseYear { get; set; }

        // YouTubeID for movie trailer
        [Required(ErrorMessage = "YouTubeID is required")]
        public string YouTubeID { get; set; }

        // Genre field
        [StringLength(maximumLength: 30, MinimumLength = 1, ErrorMessage = "The genre should have a length between 1 and 30 characters.")]
        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; }

        // IMDb Rating Field
        public float IMDbRating { get; set; }

        // Store the Comments entered by the users on this product
        // [MaxLength(30, ErrorMessage = "Comment must be 30 characters or less.")]
        // public List<Dictionary<string, string, double>> CommentList { get; set; } = new List<Dictionary<string, string, double>>();
        public List<Comment> CommentList { get; set; } = new List<Comment>();
    }

    public class Comment
    {
        public string Text { get; set; }
        public string SentimentType { get; set; }
        public double SentimentPositive { get; set; }
    }
}