using NLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.Repositories
{
    public interface ICategoryRepository:IRepository<Category>
    {
        // kategoriye özgü bir metod yazıcam. diğer metodlar IRepositoryden geliyor.

        Task<Category> GetWithProductsByIdAsync(int categoryId);



    }
}
