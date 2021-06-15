using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} alanı boş olamaz")]
        public string Name { get; set; }

        [Range(1,double.MaxValue,ErrorMessage ="{0} alanı 1'den büyük bir değer olmalıdır.")]
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        


    }
}
