using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayerProject.API.DTOs;
using NLayerProject.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.API.Filters
{
   
    public class NotFoundFilter:ActionFilterAttribute
    {
        // ben burada gidip veritabanıyla ilgili işlem yapmak için IProductService de kullanabilirim.
        // o zaman bu sadece IProductService için geçerli olur
        // ama ben IService üzerinden kullanırsam daha iyi. IService kullanırsam generic olarak productu vermem lazım
        // NotFoundFilter'im product için geçerli olmuş olcak. hmmmm ?
        
        private readonly IProductService _productService;


        // elimde product ile işlem yapacağım bir servis var.
        // burda bi DI var ben şimdi productscontrollerde getbyid de kullanmak istiyorum.
        // ben bunu kullanırsam benden parametre olarak productservice istiyor. ben bunu yapamam.
        // servis filter olarak tanımlıcam constructurı olmasa direk validationfilter gibi tanımlıcaktım.
        // startup cs ye kaydetcez

        public NotFoundFilter(IProductService productService)
        {
            _productService = productService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // id isteyen action metodlarda bu filtreyi kullanıcaz. idye gelen değeri burda yakalamak istiyorum.
            // bunlardanda bir tane olcağından dolayı FirstOrDefault metodunu kullanıyorum. bir parametresi var.
         
            int id = (int) context.ActionArguments.Values.FirstOrDefault();

            // böyle bir ürün var mı bunları tespit ettiğim kısım

            var product = await _productService.GetByIdAsync(id);

            // product diye ürün varsa 
            if(product!=null)
            {   
                // burdaki requestin devam etmesini sağlıcam. bu metoda girdi. production varsa bu kodu işlemeye devam et.
                // hangi action metodda tanımlarsam o metoda gir.
                // gelen requesti bir sonraki adıma aktarıyorum.
                await next();
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();

                //id istemiş olduğun nesne veritabanında yok.
                errorDto.Status = 404;

                // hata gösteririrz.
                errorDto.Errors.Add($"id'si {id} olan ürün veritabanında bulunamadı");

                context.Result = new NotFoundObjectResult(errorDto);
            }
        }
    }
}
