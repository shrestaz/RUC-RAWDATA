using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0._Models;
using _1._Northwind_API.Models;
using _2._Data_Layer_Abstraction;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _1._Northwind_API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private IProductRepository productRepository;
        private IMapper mapper;
        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        [HttpGet("{productId}", Name = nameof(GetProduct))]
        public ActionResult GetProduct(int productId)
        {
            var product = productRepository.GetById(productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(CreateProductDto(product));
        }

        [HttpGet("category/{categoryId}", Name = nameof(GetProductsByCategoryId))]
        public ActionResult GetProductsByCategoryId(int categoryId)
        {
            var products = productRepository.GetByCategoryId(categoryId);
            if (products.Count() == 0)
            {
                return NotFound(products);
            }
            var result = CreateResult(products);
            return Ok(result);
        }

        [HttpGet("name/{productName}", Name = nameof(GetProductsByName))]
        public ActionResult GetProductsByName(string productName)
        {
            var products = productRepository.GetByContainedSubstringInName(productName);
            if (products.Count() == 0)
            {
                return NotFound(products);
            }
            var result = CreateResult(products);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            return Ok(result);
        }

        ///////////////////
        //
        // Helpers
        //
        //////////////////////

        private ProductDto CreateProductDto(Product product)
        {
            var dto = mapper.Map<ProductDto>(product);
            dto.Link = Url.Link(
                    nameof(GetProduct),
                    new { productId = product.Id });
            return dto;
        }

        private IEnumerable<ProductDto> CreateResult(IEnumerable<Product> products)
        {
            return products.Select(p => CreateProductDto(p));
        }
    }
}
