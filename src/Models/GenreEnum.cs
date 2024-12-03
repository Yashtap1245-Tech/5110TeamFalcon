namespace ContosoCrafts.WebSite.Models
{

    /// <summary>
    /// Product Enum class
    /// </summary>
    public enum GenreEnum
    {
        Undefined = 0,
        Action = 1,
        Crime = 2,
        Drama = 3,
        Fantasy = 4,
        SciFi = 5,
        Western = 6
    }

    /// <summary>
    /// A static class that contains extension methods for the <see cref="ProductTypeEnum"/> enumeration.
    /// This class provides additional functionality to the enum, allowing for more descriptive and user-friendly 
    /// representations of the enum values, such as retrieving display names.
    /// </summary>
    /// <remarks>
    /// This class is intended to be used in conjunction with the <see cref="GenreEnum"/> enum, enabling
    /// developers to easily access display names and other related features without modifying the enum itself.
    /// </remarks>
    public static class GenereEnumExtensions
    {

        /// <summary>
        /// Retrieves the display name associated with a specific <see cref="GenreEnum"/> value.
        /// This method converts the enum value into a human-readable string, which can be useful for 
        /// displaying product types in user interfaces.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>
        /// A string representing the display name of the product type. 
        /// Returns a descriptive name for recognized enum values or an empty string for undefined or unrecognized values.
        /// </returns>
        public static string DisplayName(this GenreEnum data)
        {
            return data switch
            {
                GenreEnum.Action => "Action",
                GenreEnum.Crime => "Crime",
                GenreEnum.Drama => "Drama",
                GenreEnum.Fantasy => "Fantasy",
                GenreEnum.SciFi => "SciFi",
                GenreEnum.Western => "Western",
 
                // Default, Unknown
                _ => "",
            };

        }

    }

}