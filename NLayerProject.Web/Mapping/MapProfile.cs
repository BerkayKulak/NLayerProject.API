using AutoMapper;


using NLayerProject.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            // Kategori görürsen sen bunu CategoryDto ya dönüştür.
            //CreateMap<Category, CategoryDto>();
            //CreateMap<CategoryDto, Category>();
            //CreateMap<Category, CategoryWithProductDto>();
            //CreateMap<CategoryWithProductDto, Category>();
            //CreateMap<Product, ProductDto>();
            //CreateMap<ProductDto, Product>();
            //CreateMap<Product, ProductWithCategoryDto>();
            //CreateMap<ProductWithCategoryDto, Product>();

        }
    }
}
