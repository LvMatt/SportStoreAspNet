using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Data;
using SportStore.Dtos;
using SportStore.Model;
using SportStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;

        }

        [HttpPost]
        [Route("register")]
        public ActionResult<Customers> Register(Customers model)
        {
            if(ModelState.IsValid)
            { 
                Customers customer = new Customers
                {
                    Id = model.Id,
                    Firstname = model.Firstname,
                    Surname = model.Surname,
                    Address = model.Address,
                    Email = model.Email,
                    Password = model.Password,
                    Phone = model.Phone

                    };
                _customerRepository.Register(customer);
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "error occurred" });       
            // return _customerRepository.Register(customer);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<Customers> Login(LoginViewModel model)
        {

                var customerDetails = _customerRepository.Login(model);
                if (customerDetails == null)
                {
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "error occurred" });
                }
                HttpContext.Session.SetString("userId", customerDetails.Firstname);
                return Ok();

          }

        [HttpGet]
        [Route("customers")]
        public ActionResult<IEnumerable<Customers>> GetAllCustomers()
        {
            var customers = _customerRepository.GetAllCustomers();
            return Ok(customers);

        }

    }
}
