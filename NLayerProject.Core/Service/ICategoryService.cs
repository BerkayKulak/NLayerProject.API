using NLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.Service
{
    public interface ICategoryService:IService<Category>
    {
        Task<Category> GetWithProductsByIdAsync(int categoryId);

        // Category özgü metodlarınız varsa burada tanımlayabilirim.
    }
}
