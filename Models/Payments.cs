using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class Payments
    {
        public Payments()
        {
            Orders = new HashSet<Orders>();
        }

        public int PaymentId { get; set; }
        public int CreditCartId { get; set; }
        public string PaymentInvoice { get; set; }

        public virtual CreditCarts CreditCart { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
