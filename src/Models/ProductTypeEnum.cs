using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoCrafts.WebSite.Models
{
    public enum ProductTypeEnum
    {
        Undefined = 0,
        Amature = 1,
        Antique = 5,
        Collectable = 130,
        Commercial = 55,
    }
    /// <summary>
    /// A static class that contains extension methods for the <see cref="ProductTypeEnum"/> enumeration.
    /// This class provides additional functionality to the enum, allowing for more descriptive and user-friendly 
    /// representations of the enum values, such as retrieving display names.
    /// </summary>
    /// <remarks>
    /// This class is intended to be used in conjunction with the <see cref="ProductTypeEnum"/> enum, enabling
    /// developers to easily access display names and other related features without modifying the enum itself.
    /// </remarks>
    public static class ProductTypeEnumExtensions
    {
        /// <summary>
        /// Retrieves the display name associated with a specific <see cref="ProductTypeEnum"/> value.
        /// This method converts the enum value into a human-readable string, which can be useful for 
        /// displaying product types in user interfaces.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>
        /// A string representing the display name of the product type. 
        /// Returns a descriptive name for recognized enum values or an empty string for undefined or unrecognized values.
        /// </returns>
        public static string DisplayName(this ProductTypeEnum data)
        {
            return data switch
            {
                ProductTypeEnum.Amature => "Hand Made Items",
                ProductTypeEnum.Antique => "Antiques",
                ProductTypeEnum.Collectable => "Collectables",
                ProductTypeEnum.Commercial => "Commercial goods",
 
                // Default, Unknown
                _ => "",
            };
        }
    }
}