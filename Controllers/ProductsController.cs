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
    [ApiController]
    public class ProductsController : ControllerBase
    {
        static List<Product> products = new List<Product>()
        {
            new Product() { Id = 1, Name = "XY" },
            new Product() { Id = 2, Name = "XX" },
            new Product() { Id = 3, Name = "XXY" },
            new Product() { Id = 4, Name = "XYY" }
        };
        private readonly IProductRepository _productRepository;

        //private ProductsController(IProductRepository productRepository)
        //{
        //    _productRepository = productRepository;
        //}


        //GET api/commands
        [HttpGet]
        [Route("products")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
           // var  productItems = _productRepository.GetAllCommands();
            return products;
        }
    }
}
