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

        [HttpPost]
        [Route("orders")]
        public ActionResult CreateOrder(Orders order)
        {
            _orderRepository.CreateOrder(order);
            return Ok();
        }

        [HttpGet]
        [Route("orders/{id}")]
        public Orders GetOrderById(int id)
        {
            GetOrderDetailsById(id);
            return _orderRepository.GetOrderById(id);
        }

        [HttpGet]
        [Route("orderdetails")]
        public ActionResult<IEnumerable<Orderdetails>> GetOrderDetailsById(int id)
        {
            return Ok(_orderRepository.GetOrderDetails(id));
        }

        [HttpDelete]
        [Route("orders/{id}")]
        public ActionResult DeleteOrder(int id)
        {
            _orderRepository.DeleteOrder(id);
            return Ok();
        }
        [HttpPost]
        [Route("orderdetails")]
        public ActionResult AddToOrder(Orderdetails order)
        {
            return (_orderRepository.AddToOrder(order)) switch
            {
                1 => BadRequest("Invalid Product"),
                2 => BadRequest("Invalid Order ID"),
                _ => Ok(),
            };
        }
    }
}