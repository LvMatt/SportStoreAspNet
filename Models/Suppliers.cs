using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            Products = new HashSet<Products>();
        }

        public int SuppliersId { get; set; }
        public string SuppliersName { get; set; }
        public string SuppliersAddress { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
