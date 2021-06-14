using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.Repositories
{
    //mutlaka class olmalı
    public interface IRepository<TEntity> where TEntity:class
    {
        Task<TEntity> GetByIdAsync(int id);

        //Task yazmamızın sebebi entity framework tarafında getallasync metodunun karşılığı generic olarak olduğu için
        Task<IEnumerable<TEntity>> GetAllAsync();

        // find(x=>x.id=23) burdaki expression üzerinden entity benim yazmış olduğum sorgu üzerinden işaretlenecek
        //function delegesi ve predicate delegesi araştır ??? func delegesi
        // EntityFramework tarafında Where sorgusu generic olmadığı için async yapamayız bu yüzden task kullanmıcaz
        Task <IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);


        // category.singleOrDefaultAsyn(x=>x.name="kalem")
        //x = TEntity , x.name="kalem" ise bool ifade
        Task<TEntity> SingleOrDefaultAsnyc(Expression<Func<TEntity, bool>> predicate);

        //bir nesnenin eklenmesi
        Task AddAsync(TEntity entity);

        // birden fazla kayıdı gerçekleştirebilirim.
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        // async karşılığı yok
        void Remove(TEntity entity);

        //birden fazla kayıt silicez
        void RemoveRange(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);
        
        
    }
}
