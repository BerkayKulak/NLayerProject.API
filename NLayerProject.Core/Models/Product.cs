using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public string InnerBarcode { get; set; }

        //Entity framework bu kategori üzerinden inherit kullanarak tracking yapacak değişiklikleri izlicek. 
        public virtual Category Category { get; set; }


    }
}
