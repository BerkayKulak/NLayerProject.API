using NLayerProject.Core.Repositories;
using NLayerProject.Core.UnitOfWorks;
using NLayerProject.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ProductRepository _productRepository;
        private CategoryRepository _categoryRepository;


        // benden product istiyor. IProductRepository dönüşümü olacak. Zaten benim ProductRepository
        // IProductRepository'i implemente ettiği için dönebilirim.

        //_productRepository'i vericem. _productRepository varsa al eğer null ise git yeni bir ProductRepository oluştur.
        // bunuda _productRepository'a ata _productRepository' da Products'a ata

        //UnitofWork tarafında Products ve Categoies erişmek içinde burda nesne örneklerini üretmiş oldum.
        public IProductRepository Products => _productRepository = _productRepository ?? new ProductRepository(_context);

        public ICategoryRepository Categories =>_categoryRepository = _categoryRepository ?? new CategoryRepository(_context);

        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
