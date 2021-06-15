using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayerProject.Core.Models;
using NLayerProject.Core.Service;
using NLayerProject.Web.DTOs;
using NLayerProject.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();

            // kategori tarafında ( bu tarafta) viewe dönerken
            //_mapper.Map<IEnumerable<CategoryDto>>(categories)  daki data geliyor
            // bende index.cshtml üzerinden @model IEnumerable<CategoryDto> tarafından yakalıyorum.
            // bana IEnumerable geliyor yani tüm kategoriler geliyor.
            // c# taki tüm liste IEnumerable interfaceini implemente ettiğinden dolayı ben istediğim şekilde kullanabiliyorum.

            return View(_mapper.Map<IEnumerable<CategoryDto>>(categories));


        }

        // sayfada görünecek olan ilk yüklendiği zaman çalışacak olan metodum budur.
        public IActionResult Create()
        {
            return View();
        }

        // kullanıcı kategori ismini yazarsa ekle butonuna tıklarsa şimdi çalışacak olan ikinci butonum
        // submit butonuna basıldığında çalışacak olan metodum.
        [HttpPost]
        public async Task<IActionResult>  Create(CategoryDto categoryDto)
        {
            await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));

            //anasayfaya yönlendirme işlemini gerçekleştiricem

            return RedirectToAction("Index");

        }

        //update/5
        public async Task<IActionResult> Update(int id)
        {
            // idsi 5 olan kategoriyi elde edicem
            var category = await _categoryService.GetByIdAsync(id);
            // kategoriyi bi doldurucam çünkü update işlemi olduğundan dolayı veritabanından kategoriyi almam lazım
            return View(_mapper.Map<CategoryDto>(category));
        }

        // submit butonuna bastığı zaman
        [HttpPost]
        // update olduktan sonra cshtmlden bana ne gelecek ?
        // categoryDto nesnesi gelecek
        public IActionResult Update(CategoryDto categoryDto)
        {
            _categoryService.Update(_mapper.Map<Category>(categoryDto));
            return RedirectToAction("Index");


        }
        [ServiceFilter(typeof(NotFoundFilter))]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetByIdAsync(id).Result;
            _categoryService.Remove(category);
            return RedirectToAction("Index");


        }



    }




}
