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

        public int Id { get; set; }
        public decimal? Amount { get; set; }
        public int PaymentId { get; set; }
        public int CartId { get; set; }
        public int BranchesId { get; set; }
        public int CustomersId { get; set; }

        public virtual Branches Branches { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual Payments Payment { get; set; }
        public virtual ICollection<ProductsOrders> ProductsOrders { get; set; }
    }
}
