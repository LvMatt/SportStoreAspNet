using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.ViewModels
{
    public class RatingsViewModel
    {
        public int Id { get; set; }
        public int? Ratings { get; set; }
        public string Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int ProductsId { get; set; }
        public int CustomersId { get; set; }

        public virtual Customers Customers { get; set; }
        public virtual Products Products { get; set; }
    }
}
