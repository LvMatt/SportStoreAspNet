using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Data.Interfaces;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Controllers
{
    [Route("api/")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        [Route("orders")]
        public ActionResult<IEnumerable<Orders>> GetAllOrders()
        {
            var orderItems = _orderRepository.GetAllOrders();
            return Ok(orderItems);
        }

        [HttpGet]
        [Route("orders/{id}")]
        public Orders GetOrderById(int id)
        {
            return _orderRepository.GetOrderById(id);
        }

        [HttpGet]
        [Route("orderdetails")]
        public ActionResult<IEnumerable<Orderdetails>> GetOrderDetailsById(int id)
        {
            return Ok(_orderRepository.GetOrderDetails(id));
        }

        /*[HttpPost]
        [Route("products")]
        public ActionResult<ProductsReadDto> CreateProducts(ProductsCreateDto productCreateDto)
        {
            var productModel = _mapper.Map<Products>(productCreateDto);
            _productRepository.CreateProducts(productModel);
            _productRepository.SaveChanges();

            var productReadDto = _mapper.Map<ProductsReadDto>(productModel);

            return CreatedAtRoute(nameof(GetProductById), new { id = productReadDto.Id }, productReadDto);
        }*/

    }
}
