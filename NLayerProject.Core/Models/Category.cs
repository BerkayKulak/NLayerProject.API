using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.Models
{
    public class Category
    {
        // ilk oluşturulduğu anda kategori bir tane boş bir collection nesnesi oluştursun.
        public Category()
        {
            Product = new Collection<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Product> Product { get; set; }

    }
}
