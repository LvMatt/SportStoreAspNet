using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportStore.Models
{
    public partial class ProductCategories
    {
        public ProductCategories()
        {
            ProductsCategories = new HashSet<ProductsCategories>();
        }
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProductsCategories> ProductsCategories { get; set; }
    }
}
