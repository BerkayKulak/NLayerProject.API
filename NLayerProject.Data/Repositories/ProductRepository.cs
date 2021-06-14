using Microsoft.EntityFrameworkCore;
using NLayerProject.Core.Models;
using NLayerProject.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Data.Repositories
{
    // Repository içersinide DbContext alan bir constructor var ama biz tanımlamadığımız için kırmızı yanar
    // repository miras aldık ve içinde constructor var bu yüzden benim de mutlaka bir constructorum olmak zorunda
    // almış olduğum db contexti basde contexte gönderdim.
    // böylelikle Repositorydeki public readonly DbContext _context; (_context) kısmı dolmuş oldu.
    // _contextin de ne olduunu biliyorum bunu AppDbContext ' e cast ettim as ile beraber. artık elimde appDbContext var
    // bu sayede producta ulaşmış oldum.
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        //şimdi tanımladık ve kırmızılık söndü
        // ilgili contexti AppDbContext'e çevirirsem bana product ve category entityleri gelir
        // çünkü AppDbContext içersinde dbsetler tanımlı
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        // reposotiryden gelen bir constructor olduğu için base'e contexti gönderdim.
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        // bir id vericem bu idye sahip product dönerken aynı zamanda producta sahip kategori bilgisi de dönsün
        // çünkü benim Product.cs de birde virtual Category classım vardı. bu classımda dolsun


        public async  Task<Product> GetWithCategoryByIdAsync(int productId)

        {
            // id'si product id eşit olan ilk kaydı bul diyorum.
            // tek bir product dönerken, include metoduyla beraber ilgili kategorisinide
            // producta ekle demiş olduk
            return await _appDbContext.Products.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == productId);

        }
    }
}
