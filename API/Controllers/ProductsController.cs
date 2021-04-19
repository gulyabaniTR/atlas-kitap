using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.DbModels;
using API.Core.Interfaces;
using API.Core.Specification;
using API.Dtos;
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

        //private readonly IProductRepository _productRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;

        //Inject İşlemi Gerçekleştirildi
        public ProductsController(IGenericRepository<Product> productRepository, IGenericRepository<ProductBrand> productBrandRepository, IGenericRepository<ProductType> productTypeRepository)
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithProductTypeAndBrandsSpecification();
            var data = await _productRepository.ListAsync(spec);
            //return Ok(data);

            return data.Select(product => new ProductToReturnDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                ProductBrand = product.ProductBrand != null ? product.ProductBrand : null,
                ProductType = product.ProductType != null ? product.ProductType : null

            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithProductTypeAndBrandsSpecification(id);
            //return await _productRepository.GetEntityWithSpec(spec);
            var product = await _productRepository.GetEntityWithSpec(spec);
            return new ProductToReturnDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                ProductBrand = product.ProductBrand != null ? product.ProductBrand : null,
                ProductType = product.ProductType != null ? product.ProductType : null
            };
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            //return Ok(await _productRepository.GetProductBrandsAsync());
            return Ok(await _productBrandRepository.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            //return Ok(await _productRepository.GetProductTypesAsync());
            return Ok(await _productTypeRepository.ListAllAsync());
        }

    }
}
