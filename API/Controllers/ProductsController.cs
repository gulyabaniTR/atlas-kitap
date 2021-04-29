using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.DbModels;
using API.Core.Interfaces;
using API.Core.Specification;
using API.Dtos;
using API.Helpers;
using API.Infrastructure.DataContext;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //xxx/api/products/GetProduct
   
    public class ProductsController : BaseApiController
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

        private readonly IMapper _mapper;
        //Inject İşlemi Gerçekleştirildi
        public ProductsController(IGenericRepository<Product> productRepository, IGenericRepository<ProductBrand> productBrandRepository, IGenericRepository<ProductType> productTypeRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            var spec = new ProductsWithProductTypeAndBrandsSpecification(productSpecParams);
            var countSpec = new ProductWithFiltersForCountSpecification(productSpecParams);
            var totalItems = await _productRepository.CountAsync(spec);
            var products = await _productRepository.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            //return Ok(data);

            //return data.Select(product => new ProductToReturnDto
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Description = product.Description,
            //    PictureUrl = product.PictureUrl,
            //    Price = product.Price,
            //    ProductBrandName = product.ProductBrand != null ? product.ProductBrand.Name : string.Empty,
            //    ProductBrandId = product.ProductBrand != null ? product.ProductBrand.Id : -1,
            //    ProductTypeName = product.ProductType != null ? product.ProductType.Name : string.Empty,
            //    ProductTypeId = product.ProductType != null ? product.ProductType.Id : -1

            //}).ToList();

            return Ok(new Pagination<ProductToReturnDto>(productSpecParams.PageIndex,productSpecParams.PageSize,totalItems,data));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithProductTypeAndBrandsSpecification(id);
            //return await _productRepository.GetEntityWithSpec(spec);
            var product = await _productRepository.GetEntityWithSpec(spec);
            //return new ProductToReturnDto
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Description = product.Description,
            //    PictureUrl = product.PictureUrl,
            //    Price = product.Price,
            //    ProductBrandName = product.ProductBrand != null ? product.ProductBrand.Name : string.Empty,
            //    ProductBrandId = product.ProductBrand != null ? product.ProductBrand.Id : -1,
            //    ProductTypeName = product.ProductType != null ? product.ProductType.Name : string.Empty,
            //    ProductTypeId = product.ProductType != null ? product.ProductType.Id : -1
            //};

            return _mapper.Map<Product, ProductToReturnDto>(product);
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
