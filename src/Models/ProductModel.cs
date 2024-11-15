using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Models
{
    public class ProductModel
    {
        //ID Field
        public string Id { get; set; }
        //Maker Field
        public string Maker { get; set; }

        //Image Field
        [JsonPropertyName("img")]
        public string Image { get; set; }
        //Url Field
        public string Url { get; set; }
        //Title Field
        [StringLength (maximumLength: 50, MinimumLength = 1, ErrorMessage = "The Title should have a length of more than {2} and less than {1}")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        // Director Field
        [Required(ErrorMessage = "Director is required")]
        public string Director { get; set; }
        //Description Field
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        //Ratings Field
        [Required(ErrorMessage = "Ratings are required")]
        public int[] Ratings { get; set; }
        //Cast Field
        [Required(ErrorMessage = "Cast is required")]
        public List<string> Cast { get; set; }
        public ProductTypeEnum ProductType { get; set; } = ProductTypeEnum.Undefined;
        //Quantity Field
        public string Quantity { get; set; }

        [Range (-1, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        //Price Field
        public int Price { get; set; }
        //Release Year Field
        public int ReleaseYear { get; set; }
        //YoutubeID for movie trailer
        [Required(ErrorMessage = "YouTubeID is required")]
        public string YouTubeID { get; set; }
        //Genre field
        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; }
        //IMDbRating Field
        public float IMDbRating { get; set; }
        // Store the Comments entered by the users on this product
        public List<string> CommentList { get; set; } = new List<string>();
        //JSON Serializer
        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);

    }
}