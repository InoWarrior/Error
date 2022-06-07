using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiFHT.Models;
using WebApiFHT.Services;

namespace WebApiFHT.Controllers
{
    [Route("api/fht/{CompanyId}/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public ActionResult Post([FromRoute] int CompanyId,[FromBody] CreateProductDto dto)
        {
            var newProductId = _productService.Create(CompanyId, dto);


            return Created($"api/fht/{CompanyId}/dish/{newProductId}", null);
        }

        [HttpGet("{productId}")]
        public ActionResult<ProductDto> Get([FromRoute] int CompanyId,[FromRoute] int productId)
        {
            ProductDto product = _productService.GetById(CompanyId, productId);

            return Ok(product);
        }

        [HttpGet]
        public ActionResult<List<ProductDto>> Get([FromRoute] int CompanyId)
        {
            var result = _productService.GetAll(CompanyId);

            return Ok(result);
        }
        [HttpDelete]
        public ActionResult Delete([FromRoute] int companyId)
        {
            _productService.RemoveAll(companyId);

            return NoContent();
        }
    }
}
