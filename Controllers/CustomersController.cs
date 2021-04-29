using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Data;
using SportStore.Dtos;
using SportStore.Models;
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
        public ActionResult<CustomersReadDto> Register(CustomersRegisterDto registerCreateDto)
        {
            if (ModelState.IsValid)
            {
                 var customerModel = _mapper.Map<Customers>(registerCreateDto);
                _customerRepository.Register(customerModel);
                _customerRepository.SaveChanges();

                var customerReadDto = _mapper.Map<CustomersReadDto>(customerModel);
                return CreatedAtRoute(new { Id = customerReadDto.Id }, customerReadDto);
            }


            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "error occurred" });
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

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            if (HttpContext.Session != null)
                BadRequest("Session is unset");
            HttpContext.Session.Clear();
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
