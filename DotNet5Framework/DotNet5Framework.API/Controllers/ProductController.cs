using DotNet5Framework.Business.Abstract;
using DotNet5Framework.Entities.Dtos.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet5Framework.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public IActionResult GettAll()
        {
            var result = _productService.GetAll();
            return Ok(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(ProductAddDto dto)
        {
            var result = _productService.Add(dto);
            return Ok(result);
        }

        [HttpGet("TransactionTest")]
        public IActionResult TransactionTest()
        {
            var result = _productService.TransactionTest();
            return Ok(result);
        }
    }
}
