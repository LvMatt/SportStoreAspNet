using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Data;
using SportStore.Dtos;
using SportStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
       
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("products")]
        public ActionResult<IEnumerable<Products>> GetAllProducts()
        {
            var productItems = _productRepository.GetAllProducts();
            return Ok(_mapper.Map<IEnumerable<ProductsReadDto>>(productItems));
           
        }

        [HttpGet("products/{id}")]
        public ActionResult<ProductsReadDto> GetProductById(int id)
        {
            var productItems = _productRepository.GetProductById(id);
            if (productItems != null)
            {
                return Ok(_mapper.Map<ProductsReadDto>(productItems));
            }
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        [Route("products")]
        public ActionResult<ProductsReadDto> CreateProducts(ProductsCreateDto productCreateDto)
        {
            var productModel = _mapper.Map<Products>(productCreateDto);
            _productRepository.CreateProducts(productModel);
            _productRepository.SaveChanges();

            var productReadDto = _mapper.Map<ProductsReadDto>(productModel);

            return CreatedAtRoute(nameof(GetProductById), new { Id = productReadDto.Id }, productReadDto);
        }
    }
}
