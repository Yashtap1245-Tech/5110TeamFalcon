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
        public string Maker { get; set; }

        [JsonPropertyName("img")]
        public string Image { get; set; }

        public string Url { get; set; }
        
        [StringLength (maximumLength: 33, MinimumLength = 1, ErrorMessage = "The Title should have a length of more than {2} and less than {1}")]
        public string Title { get; set; }
        // Director Field
        public string Director { get; set; }
        //Description Field
        public string Description { get; set; }
        //Ratings Field
        public int[] Ratings { get; set; }
        //Cast Field
        public List<string> Cast { get; set; }
        //TrailerUrl Field
        public string TrailerUrl { get; set; }
        public ProductTypeEnum ProductType { get; set; } = ProductTypeEnum.Undefined;
        //Quantity Field
        public string Quantity { get; set; }

        [Range (-1, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        //Price Field
        public int Price { get; set; }
        //YoutubeID
        public string YouTubeID { get; set; }
        //Genre field
        public string Genre { get; set; }
        // Store the Comments entered by the users on this product
        public List<CommentModel> CommentList { get; set; } = new List<CommentModel>();

        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);


    }
}