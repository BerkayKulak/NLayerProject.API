using Microsoft.EntityFrameworkCore;
using NLayerProject.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Data.Repositories
{
    
    // burda belirteceğim generic ifade mutlaka bir class olmak zorunda
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        // protected olarak belirledim çünkü miras aldığım yerde yani ProductRepositoryrde kullanıyorum.
        protected readonly DbContext _context;

        // bu arkadaşı da sadece burda kullandığımdan dolayı private yaptım
        private readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }


        public async Task AddAsync(TEntity entity)
        {
            // bundan sonra yazacağım metod bitene kadar bu satırda bekle diyorum.
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            //burdaki AddRangeAsync gibi metodlar entityframework tarafından geliyor
            await _dbSet.AddRangeAsync(entities);
        }
       
        // product.Where(x=>x.name="ahmet")
        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        //sadece ilgili nesne döner ya product ya kategori ya da ... order filan döner
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        //entity alan geriyede bool dönen bana bir metod ver
        public async Task<TEntity> SingleOrDefaultAsnyc(Expression<Func<TEntity, bool>> predicate)
        {
            // ilk gelen kaydı bana getir yoksa defaultunu getir
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public TEntity Update(TEntity entity)
        {
            //entity framework gelen bu entitiynin state durumunu modifiye olarak gördükten sonra
            // gidicek bunu veritabanına save change metodunu nerde kullanırsam veritabanına bu entity yansıtacak
            // özellikle çok satırlı  çok sütunlu tablolarımız varsa bu keywordü kullanmak uygun olacaktır
            //dezavantajı ise entityde tek bir alanı değiştirsek dahi tüm alanı update edecek şekilde sorgu gönderir.
            _context.Entry(entity).State = EntityState.Modified;
            // enttiy.name = product.name
            // entity.price = product.price 
            // böyle tek tek yazmak yerine tek bir metodda alt alta yazmaktan kurtuluruz.
            // performans kaybı göz ardı edilebilir. hepsine sorgu gönderecek böyle ama olsun.

            return entity;
        }
    }
}
