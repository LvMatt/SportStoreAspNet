using SportStore.Models;
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
            Orderdetails = new HashSet<Orderdetails>();
            Productratings = new HashSet<Productratings>();
        }

        public int Id { get; set; }
        public int SuppliersId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
        public decimal? Weight { get; set; }
        public string ImageName { get; set; }
        public int ProductCategoriesId { get; set; }
        public float? OverallRating { get; set; }

        public virtual Productcategories ProductCategories { get; set; }
        public virtual Suppliers Suppliers { get; set; }
        public virtual ICollection<Orderdetails> Orderdetails { get; set; }
        public virtual ICollection<Productratings> Productratings { get; set; }
    }
}
