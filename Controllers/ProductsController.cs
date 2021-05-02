using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    [Route("api/products")]
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

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public ActionResult<IEnumerable<Products>> GetAllProducts()
        {
            var productItems = _productRepository.GetAllProducts();
            return Ok(_mapper.Map<IEnumerable<ProductsReadDto>>(productItems));
        }

        [HttpGet]
        [Route("sort/{sortType}")]
        public ActionResult<IEnumerable<Products>> GetAllProductsBySort(string sortType)
        {
            var productItems = _productRepository.SortProductsByType(sortType);
            return Ok(productItems);
        }

        [HttpGet]
        [Route("sort={sortType}/search={productParameter}")]
        public ActionResult<IEnumerable<Products>> SerachProduct(string sortType, string productParameter)
        {
            var products = _productRepository.GetAllProducts();
            if (productParameter == "")
                return Ok(products);
            var searchedProduct = _productRepository.SearchFilterProduct(products, sortType,productParameter);
            return Ok(_mapper.Map<IEnumerable<ProductsReadDto>>(searchedProduct));
        }



        [HttpGet("{id}")]
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
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
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
