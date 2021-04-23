using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class Orders
    {
        public Orders()
        {
            ProductsOrders = new HashSet<ProductsOrders>();
        }

        public int OrdersId { get; set; }
        public decimal? OrdersFullAmount { get; set; }
        public int PaymentId { get; set; }
        public int CartCartId { get; set; }
        public int BranchesBranchesId { get; set; }
        public int CustomersCustomerId { get; set; }

        public virtual Branches BranchesBranches { get; set; }
        public virtual Customers CustomersCustomer { get; set; }
        public virtual Payments Payment { get; set; }
        public virtual ICollection<ProductsOrders> ProductsOrders { get; set; }
    }
}
