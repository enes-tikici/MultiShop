using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponent
{
    public class _ProductDetailImageSliderComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _ProductDetailImageSliderComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var client = _httpClientFactory.CreateClient();

            // URL'deki ?id= kısmını silip sonuna direkt ID'yi ekle
            var responseMessage = await client.GetAsync("https://localhost:7070/api/ProductImages/ProductImagesByProductId/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                var jObject = Newtonsoft.Json.Linq.JObject.Parse(jsonData);
                var values = new GetByIdProductImageDto
                {
                    ProductImageId = jObject["productImageId"]?.ToString(),
                    Image1 = jObject["image1"]?.ToString(),
                    Image2 = jObject["image2"]?.ToString(),
                    Image3 = jObject["image3"]?.ToString(),
                    Image4 = jObject["image4"]?.ToString(),
                    ProductId = jObject["productId"]?.ToString()
                };



                return View(values);
            }
            return View();
        }
    }
}
