using IndataProje.Models;
using IndataProje.Repository.Abstract;
using IndataProje.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IndataProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class ProductController : ControllerBase
    {
        public IProductRepository _IProductRepository { get; set; }

        public ProductController()
        {
            _IProductRepository=new ProductRepository();
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            return Ok(_IProductRepository.GetAll());
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetByIdProduct(int id)
        {
            return Ok(_IProductRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Products products)
        {
            return Ok(_IProductRepository.Create(products));
        }

        [HttpPut]
        public IActionResult UpdateProduct([FromBody] Products product)
        {
            return Ok(_IProductRepository.Update(product));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            return Ok(_IProductRepository.Delete(id));
        }
    }
}
