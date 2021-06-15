using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.DTOs
{
    public class CategoryWithProductDto:CategoryDto
    {
        // productlarıda alan yeni bir dto nesnesi oluşturduk.
        // aynı propertyleri yazmak yerine CategoryDto'daki mirası alarak propertyleri otomatik olarak almış oldum.
        // propertylerin isimleri birebir aynısı olması lazım category.cs de products değil Products olarak yazılmış.
        public ICollection<ProductDto> Products { get; set; }
    }
}
