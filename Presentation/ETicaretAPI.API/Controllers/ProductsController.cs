using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            //await _productWriteRepository.AddRangeAsync(new()
            //{
            //    new() {Id=Guid.NewGuid(), Name="Product 1", Price=100, CreatedDate=DateTime.UtcNow,Stock=10},
            //    new() {Id=Guid.NewGuid(), Name="Product 2", Price=200, CreatedDate=DateTime.UtcNow,Stock=10},
            //    new() {Id=Guid.NewGuid(), Name="Product 3", Price=300, CreatedDate=DateTime.UtcNow,Stock=10}
            //});
            //await _productWriteRepository.SaveAsync();

            Product p = await _productReadRepository.GetByIdAsync("2b137644-3043-4cd5-bca2-3a4554df63d9",false);
            p.Name = "Mehmet";
            await _productWriteRepository.SaveAsync();
            //ProductReadRepository ile elde etiğimiz veriyi ProductWriteRepository ile SaveAsync yapabilmemizi sağlayan sistem -> AddScoped sistemidir.
            //ServiceRegistration.cs içinde tüm repositoryler (DbContext default olarak AddScoped kullanılıyor) Scoped olduğundan dolayı çalışacaktır.
            //ProductWriteRepository, id Talep eden ile aynı instance'i kullanacaktır
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
