using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Data;
using SportStore.Dtos;
using SportStore.Models;
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

        /*[Authorize]*/
        [HttpPost]
        [Route("products")]
        public ActionResult<ProductsReadDto> CreateProducts(ProductsCreateDto productCreateDto)
        {
            var productModel = _mapper.Map<Products>(productCreateDto);
            _productRepository.CreateProducts(productModel);
            _productRepository.SaveChanges();

            var productReadDto = _mapper.Map<ProductsReadDto>(productModel);

            return CreatedAtRoute(nameof(GetProductById), new { id = productReadDto.Id }, productReadDto);
        }

        [HttpPost]
        [Route("review/product/{pId}/user/{uId}")]
        public ActionResult<ProductsReadDto> ReivewProduct(Productratings ratings, int pId, int uId)
        {
            _productRepository.CreateProductReview(ratings, uId, pId);
            _productRepository.SaveChanges();
            return Ok();
        }




        [Authorize]
        [HttpPost]
        [Route("products/buy/{id}")]
        public ActionResult AddToCart(int id)
        {
            return Ok();
        }

        [HttpGet]
        [Route("product/ratings")]
        public IEnumerable<Productratings> GetAllProductRatings()
        {
            var productRatings = _productRepository.GetAllProductRatings();
            return productRatings;
        }


        [HttpGet]
        [Route("product/ratings/{pId}")]
        public IQueryable<string> GetAllRatingsByProductId(int pId)
        {
            var productRating = _productRepository.GetAllProductRatingsByProductId(pId);
            return productRating;
        }

        /*[Authorize]*/
        [HttpPut]
        [Route("products/{id}")]
        public ActionResult UpdateProducts(Products productModel)
        {
            /*var productModel = _mapper.Map<Products>(productCreateDto);*/
            _productRepository.UpdateProduct(productModel);
            _productRepository.SaveChanges();

           /* var productReadDto = _mapper.Map<ProductsReadDto>(productModel);*/

            return Ok();
        }

        [HttpDelete]
        [Route("products/{id}")]
        public ActionResult DeleteProducts(int id)
        {
            _productRepository.DeleteProduct(id);
            _productRepository.SaveChanges();
            return Ok();
        }

    }
}
