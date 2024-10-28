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