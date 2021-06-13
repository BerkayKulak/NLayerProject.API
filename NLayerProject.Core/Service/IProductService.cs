using NLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.Service
{
    public interface IProductService:IService<Product>
    {
        // bu metod herhangi bir veritabanı ile ilgisi yok. gidicek dış taraftaki bir api ile (implementasyonunu yazdığım zaman)
        // ordaki barkod ile içerdeki barkodu karşılaştırıcak eğer doğruysa true yanlışsa false dönücek
        // o yüzden service oluşturuyorum. illaki veritabanı olcak diye bir şey yok kendi içinde hesaplamaları olabilir.
        // hem Iservice üzerinden gelen veritabanı ile işlemler hem de veritabanı dışından yapacağımız işlemler olur.
        //bool ControlInnerBarcode(Product product);
        Task<Product> GetWithCategoryByIdAsync(int productId);
    }
}
