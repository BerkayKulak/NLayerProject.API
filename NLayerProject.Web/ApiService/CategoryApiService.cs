using Newtonsoft.Json;
using NLayerProject.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Web.ApiService
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            IEnumerable<CategoryDto> categoryDtos;
            // baseUrl zaten geliyor. ben sadece categories yazıcam
            // "https://localhost:44351/api/categories" oldu
            var response = await _httpClient.GetAsync("categories");

            if(response.IsSuccessStatusCode)
            {
                //json datayı IEnumerable<CategoryDto> categoryDtos; burdaki sınıfıma cast(dönüştürme) yapmam lazım
                // ReadAsStringAsync ifadesi ile json ifadeyi okuyorum.
                categoryDtos = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(await response.Content.ReadAsStringAsync());


            }
            else
            {
                categoryDtos = null;
            }

            return categoryDtos;

        }

        public async Task<CategoryDto> AddAsync(CategoryDto categoryDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(categoryDto),Encoding.UTF8,"application/json");

            var response = await _httpClient.PostAsync("categories", stringContent);

            if(response.IsSuccessStatusCode)
            {
                //serialize = bir nesneyi bir jsona dönüştürme
                //deserialize = bir jsonu bir classa dönüştürme

                categoryDto = JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());
                return categoryDto;
            }

            else
            {
                //loglama yap
                return null;
            }
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"categories/{id}");

            if(response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Update(CategoryDto  categoryDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(categoryDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("categories", stringContent);

            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Remove(int id)
        {
            var response = await _httpClient.DeleteAsync($"categories/{id}");
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            {
                return false;
            }

        }

    }
}
