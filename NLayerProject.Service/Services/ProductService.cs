using NLayerProject.Core.Models;
using NLayerProject.Core.Repositories;
using NLayerProject.Core.Service;
using NLayerProject.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {


        //private readonly IUnitOfWork _unitOfWork;
        //// dependencjy injection kullanıyoruz. Constructorda IUnitOfWork geçtiğim zaman bana 
        //// UnitOfWork classından bir nesne örneği verecek.
        //// _unitOfWork üzerinden hem productlara hem de categorilere erişebiliyorum.
        //// UnitOfWork üzerinde public olan 
        ////  *public IProductRepository Products => _productRepository = _productRepository ?? new ProductRepository(_context)
        ////  *public ICategoryRepository Categories => _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);
        ////  ifadeleri olduğu için

        //public ProductService(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;

        //}
        //public async Task<Product> AddAsync(Product entity)
        //{
        //    await _unitOfWork.Products.AddAsync(entity);

        //    // içinde savechange metodu çalışıyor.
        //    await _unitOfWork.CommitAsync();

        //    return entity;
        //}

        //public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        //{
        //    await _unitOfWork.Products.AddRangeAsync(entities);
        //    await _unitOfWork.CommitAsync();
        //    return entities;
        //}

        //public async Task<IEnumerable<Product>> Where(Expression<Func<Product, bool>> predicate)
        //{
        //    return await _unitOfWork.Products.Where(predicate);
        //}

        //public async Task<IEnumerable<Product>> GetAllAsync()
        //{
        //    return await _unitOfWork.Products.GetAllAsync();
        //}

        //public async Task<Product> GetByIdAsync(int id)
        //{
        //    return await _unitOfWork.Products.GetByIdAsync(id);
        //}

        //public async Task<Product> GetWithCategoryByIdAsync(int productId)
        //{
        //    return await _unitOfWork.Products.GetWithCategoryByIdAsync(productId);
        //}

        //public void Remove(Product entity)
        //{
        //    _unitOfWork.Products.Remove(entity);
        //    _unitOfWork.Commit();
        //}

        //public void RemoveRange(IEnumerable<Product> entities)
        //{
        //    _unitOfWork.Products.RemoveRange(entities);
        //    _unitOfWork.Commit();
        //}

        //public async Task<Product> SingleOrDefaultAsnyc(Expression<Func<Product, bool>> predicate)
        //{
        //    return await _unitOfWork.Products.SingleOrDefaultAsnyc(predicate);
        //}

        //public Product Update(Product entity)
        //{

        //    var updateProduct = _unitOfWork.Products.Update(entity);
        //    _unitOfWork.Commit();
        //    return updateProduct;


        //}
        //// sorgulama metodlarında commit'e gerek yok (savechanges)
        ///

        ////


        public ProductService(IUnitOfWork unitOfWork, IRepository<Product> repository) : base(unitOfWork, repository)
        {

        }


        public async Task<Product> GetWithCategoryByIdAsync(int productId)
        {
            return await _unitOfWork.Products.GetWithCategoryByIdAsync(productId);

        }
    }
}
