using SportStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Dtos
{
    public class ProductsCreateDto
    {
        public ProductsCreateDto()
        {
            ProductsCategories = new HashSet<ProductCategories>();
            ProductsOrders = new HashSet<Orderdetails>();
        }

        public int Id { get; set; }
        public int SuppliersId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
        public decimal? Weight { get; set; }
        public string ImageName { get; set; }

        public virtual Suppliers Suppliers { get; set; }
        public virtual ICollection<ProductCategories> ProductsCategories { get; set; }
        public virtual ICollection<Orderdetails> ProductsOrders { get; set; }
    }
}
