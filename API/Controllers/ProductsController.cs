using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.DbModels;
using API.Core.Interfaces;
using API.Infrastructure.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //xxx/api/products/GetProduct
    [Route("api/[controller]")] 
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // ESKİ

        //private readonly StoreContext _context;
        //public ProductsController(StoreContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet]
        //public ActionResult<List<Product>> GetProducts()
        //{
        //    var data = _context.Products.ToList();
        //    return data;
        //}

        //[HttpGet("{id}")]
        //public ActionResult<Product> GetProduct(int id)
        //{
        //    return _context.Products.Find(id);
        //}

        ////asenkron dönüşümü
        //[HttpGet]
        //public async Task<ActionResult<List<Product>>> GetProducts()
        //{
        //    var data = await _context.Products.ToListAsync();
        //    return data;
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Product>> GetProduct(int id)
        //{
        //    return await _context.Products.FindAsync(id);
        //}

        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {   
            var data = await _productRepository.GetProductsAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }
    }
}
