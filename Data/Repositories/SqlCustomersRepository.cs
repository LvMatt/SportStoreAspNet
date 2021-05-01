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

        public async Task<AuthenticationResult> Login(LoginViewModel customer)
        {
            var encPassword = Encryptdata(customer.Password);
            var decPassword = Decryptdata(encPassword);
            var user =  _context.Customers.SingleOrDefault(m => m.Email == customer.Email && m.Password == encPassword);
            return GenerateToken(user);  
        }

        public string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }

        public static string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
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
                    var encPassword = Encryptdata(customer.Password);
                    // var newEncPassword = _context.Customers.FirstOrDefault(x => x.Password == encPassword);
                    customer.Password = encPassword.ToString();
                    _context.Customers.Add(customer);
                    _context.SaveChanges();

            return GenerateToken(customer);

                    
                //catch (Exception ex)
                //{
                //    transaction.Rollback();
                //    throw ex;
                //}
}
        private AuthenticationResult GenerateToken(Customers customer)
        {
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
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
