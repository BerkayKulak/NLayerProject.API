using NLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.Repositories
{
    public interface IProductRepository:IRepository<Product>
    {
        // ben herhangi bir ürünün idsine göre product nesnesi almak istediğim zaman
        //o product'a bağlı kategoride gelsin. nasıl gelecekl Product cs de  public virtual Category Category { get; set; } ifadesi gelsin

        Task<Product> GetWithCategoryByIdAsync(int productId);
    }
}
