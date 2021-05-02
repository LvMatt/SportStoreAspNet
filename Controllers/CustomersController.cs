using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Data;
using SportStore.Dtos;
using SportStore.Models;
using SportStore.Options;
using SportStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SportStore.Controllers
{
    [Route("api/customers/")]
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
        public async Task<IActionResult> Register(CustomersRegisterDto registerCreateDto)
        {
            if (ModelState.IsValid)
            {
                var customerModel = _mapper.Map<Customers>(registerCreateDto);
                var authResponse = await _customerRepository.Register(customerModel);
                if(!authResponse.Success)
                {
                    return BadRequest(new AuthFailedResponse
                    {
                        Errors = authResponse.Errors
                    });
                }
                _customerRepository.SaveChanges();
                _mapper.Map<CustomersReadDto>(customerModel);
                return Ok(new AuthSuccessResponse
                {
                    Token = authResponse.Token
                });
                //return CreatedAtRoute(new { Id = customerReadDto.Id }, customerReadDto);

            }
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "error occurred" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

                var customerDetails =  await _customerRepository.Login(model);
                if (customerDetails == null)
                {
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "error occurred" });
                }
                //HttpContext.Session.SetString("userId", customerDetails.Firstname);
                return Ok(new AuthSuccessResponse { 
                    Token = customerDetails.Token
                });

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


  //      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public ActionResult<IEnumerable<Customers>> GetAllCustomers()
        {
            var customers = _customerRepository.GetAllCustomers();
            return Ok(customers);

        }

    }
}
