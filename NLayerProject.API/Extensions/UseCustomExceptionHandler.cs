using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLayerProject.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.API.Extensions
{
    // extension olduğu için static olmalı
    // extension metodlar = net framework tarafında var olan objelerin üzerine yazdığımız ekstra metodlardır.
    // IWebHostEnvironment nesnesine bir extension metod yazarsak o da gözükür.
    // biz IApplicationBuilder üzerine bir extension metod yazıcaz. okunabilirlik artıcak.
    public static class UseCustomExceptionHandler
    {
        // artık IApplicationBuilder üzerinden benim bu metodum gözükecek
        public static void UseCustomException(this IApplicationBuilder app)
        {
            //middleware
            // options istiyordu confige lambda ile girdim.
            // eğer UseExceptionHandler içerisine girdim.
            // 
            app.UseExceptionHandler(config =>
            {
                // hata fırladığı zaman run metoduyla çalışacak olan işlemlerimi gerçekleştiricem.
                // run metodu da benden requeset delege istiyor. request delegeyi temsil etmesi açısından
                // context ismini verdim. yine lambda ile beraber içerisine girdim.
                // burdaki async koymamızın sebei ise WriteAsync olduğundan dolayı
                config.Run(async context =>
                {
                    // bu hatalar serverde olcağından dolayı 500 ile başlamak durumunda.

                    context.Response.StatusCode = 500;
                    // json olarak döndüğüm için böyle
                    context.Response.ContentType = "application/json";
                    //hatayı yakalıcam. uygulama içinde hata fırlarsa aldım hatayı
                    // IExceptionHandlerFeature dönüyor
                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        // geriye exception dönüyor
                        var ex = error.Error;
                        ErrorDto errorDto = new ErrorDto();
                        errorDto.Status = 500;
                        errorDto.Errors.Add(ex.Message);

                        // dönme işlemini gerçekleştirelim. yani responsei yazdırcam
                        // SerializeObject(errorDto) burdaki nesnemi jsona dönüştürücek.
                        // PersonsController içinde mesela Return Ok(persons ) yazınca 
                        // entity framework otomatik bir şekilde çevirme işlemini gerçekleştiriyor.
                        // biz manuel çevirmek zorundayız.
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorDto));

                    }
                });

            });
        }
    }
}
