using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class CreditCarts
    {
        public CreditCarts()
        {
            Payments = new HashSet<Payments>();
        }

        public int CreditCartId { get; set; }
        public string CreditcartValidation { get; set; }
        public DateTime CreditcartExpiration { get; set; }
        public string CreditcartExtradetails { get; set; }
        public int CustomerCustomerId { get; set; }

        public virtual Customers CustomerCustomer { get; set; }
        public virtual ICollection<Payments> Payments { get; set; }
    }
}
