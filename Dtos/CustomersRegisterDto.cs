using SportStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Dtos
{
    public class CustomersRegisterDto
    {
        public CustomersRegisterDto()
        {
            CreditCarts = new HashSet<Creditcards>();
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Creditcards> CreditCarts { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
