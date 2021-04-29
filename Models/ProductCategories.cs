using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class Productcategories
    {
        public Productcategories()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
