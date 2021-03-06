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

        public int Id { get; set; }
        public int CreditcartId { get; set; }
        public string PaymentInvoice { get; set; }

        public virtual Creditcards Creditcart { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
