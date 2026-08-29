using Microsoft.AspNetCore.Mvc;
using ProjectKubRab.API.Core.Interfaces.Application.Services;
using ProjectKubRab.API.Core.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectKubRab.API.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productApplicationService) : ControllerBase
    {
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> Get()
        {
            return await productApplicationService.GetAllAsync();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ProductViewModel> Get(string id)
        {
            return await productApplicationService.GetByIdAsync(id);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<bool> Post([FromBody] ProductViewModel product)
        {
            return await productApplicationService.InsertAsync(product);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<bool> Put(string id, [FromBody] ProductViewModel product)
        {
            return await productApplicationService.UpdateAsync(product);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await productApplicationService.DeleteAsync(id);
        }
    }
}
