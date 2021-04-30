using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SportStore.Models;
using SportStore.Options;
using SportStore.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Data
{
    public class SqlCustomersRepository : ICustomerRepository
    {
        private readonly SportStoreContext _context;
        private readonly JwtSettings _jwtSettings;

        public SqlCustomersRepository(SportStoreContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        public IEnumerable<Customers> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customers GetCustomerById(int id)
        {
            return _context.Customers.FirstOrDefault(p => p.Id == id);
        }

        public Customers Login(LoginViewModel customer)
        {
            return _context.Customers.SingleOrDefault(m => m.Firstname == customer.Firstname && m.Password == customer.Password);
        }

        public async Task<AuthenticationResult> Register(Customers customer)
        {
            //using (var transaction = _context.Database.BeginTransaction())
            //{
            //    try
            //    {
                    var existingUser = _context.Customers.SingleOrDefault(m => m.Email == customer.Email);
                    if (existingUser != null)
                    {
                        return new AuthenticationResult
                        {
                            Errors = new[] { "User with this email already exists" }
                        };
                    }
                    if (customer == null)
                    {
                        return new AuthenticationResult
                        {
                            Errors = new[] { "You didnt fill form validation" }
                        };
                    }
                    _context.Customers.Add(customer);
                    _context.SaveChanges();

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, value: customer.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Email, value: customer.Email)
                            //new Claim(type: "id" , value: customer.Id),
                        }),
                        Expires = DateTime.UtcNow.AddHours(2),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    // transaction.Commit();
                    return new AuthenticationResult
                    {
                        Success = true,
                        Token = tokenHandler.WriteToken(token)
                    };
                //}
                //catch (Exception ex)
                //{
                //    transaction.Rollback();
                //    throw ex;
                //}
}

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
