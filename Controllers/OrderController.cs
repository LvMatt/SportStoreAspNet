using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Data.Interfaces;
using SportStore.Models;

namespace SportStore.Controllers
{
    [Route("api/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
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
    }
}