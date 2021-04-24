using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Data;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Controllers
{
    //api/commands
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


        //GET api/commands
        [HttpGet]
        [Route("products")]
        public ActionResult<IEnumerable<Products>> GetAllProducts()
        {
            var productItems = _productRepository.GetAllProducts();
            return Ok(productItems);
            //return Ok(productItems);
            //return products;
        }

        //GET api/commands/{id}
        //[HttpGet("{id}", Name = "GetProductById")]
        //public ActionResult<ProductsReadDto> GetProductById(int id)
        //{
        //    var productItems = _productRepository.GetProductById(id);
        //    if (productItems != null)
        //    {
        //        return Ok(_mapper.Map<ProductsReadDto>(productItems));
        //    }
        //    return NotFound();
        //}
    }
}
