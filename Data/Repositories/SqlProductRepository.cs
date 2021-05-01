using Microsoft.EntityFrameworkCore;
using SportStore.Models;
using SportStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Data
{
    public class SqlProductRepository : IProductRepository
    {
        private readonly SportStoreContext _context;

        public SqlProductRepository(SportStoreContext context)
        {
            _context = context;
        }


        public Products GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Products> GetAllProducts()
        {
            return _context.Products.ToList();
        }

       

        public void CreateProducts(Products product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Add(product);
        }

        public void UpdateProduct(Products newproduct)
        {
            if (newproduct == null)
            {
                throw new ArgumentNullException(nameof(newproduct));
            }

            var oldproduct = _context.Products.FirstOrDefault(p => p.Id == newproduct.Id);
            if (oldproduct != null)
            {
                oldproduct.Id = newproduct.Id;
                oldproduct.SuppliersId = newproduct.SuppliersId;
                oldproduct.Name = newproduct.Name;
                oldproduct.Price = newproduct.Price;
                oldproduct.Description = newproduct.Description;
                oldproduct.ImageData = newproduct.ImageData;
                oldproduct.Weight = newproduct.Weight;
                oldproduct.ImageName = newproduct.ImageName;
            }
        }

        public void DeleteProduct(int id)
        {
            var deletedproduct = _context.Products.FirstOrDefault(p => p.Id == id);
            _context.Products.Remove(deletedproduct);
        }

        public IEnumerable<Customers> CustomerDetails()
        {
            var customers =_context.Customers.ToList();
            return customers;
        }

        public void CreateProductReview(Productratings ratings, int userId, int productId)
        {
            var pId = _context.Products.FirstOrDefault(p => p.Id == productId);
            var cId = _context.Customers.FirstOrDefault(p => p.Id == userId);

            ratings.CustomersId = cId.Id;
            ratings.ProductsId = pId.Id;

            if (_context.Productratings.Any(c => c.CustomersId == userId))
            { 
                return;
            }


            _context.Productratings.Add(ratings);

        }

        public IEnumerable<Productratings> GetAllProductRatings()
        {
            var ratings = _context.Productratings.ToList();
            var ratingsComments = _context.Productratings.Include(x => x.Products).Include(x => x.Customers);
            return ratingsComments;
            // _context.Productratings.Include(c => c.ProductsId).ToList();
            // _context.Productratings.Include(c => c.Products).ToList();
        }



        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IQueryable<string> GetAllProductRatingsByProductId(int id)
        {
            var productRatings = _context.Productratings
                .Include(x => x.Customers)
                .Where(x => x.ProductsId == id);
            var selectComments = productRatings
                .Select(x  => x.Comment );
            return selectComments;

        }

        public IEnumerable<Products> SortProductsByType(string sortType)
        {
            var products = _context.Products;
            switch (sortType)
            {
                case "priceUp":
                    return products.OrderByDescending(x => x.Price);
                case "priceDown":
                    return products.OrderBy(x => x.Price);
                default:
                    break;
            }
            return products;
        }

        public List<Products> SearchFilterProduct(IEnumerable<Products> products, string sortType, string searchParam)
        {
            // var products = _context.Products.ToList();

            if (String.IsNullOrEmpty(searchParam) || searchParam == "")
            {
                return products.ToList();
                // owners = owners.Where(o => o.Name.ToLower().Contains(ownerName.Trim().ToLower()));
            }
            products = products.Where(o => o.Name.ToLower().Contains(searchParam.Trim().ToLower()));
            switch (sortType)
            {
                case "priceUp":
                    return products.OrderByDescending(x => x.Price).ToList();
                case "priceDown":
                    return products.OrderBy(x => x.Price).ToList();
                default:
                    break;
            }

            return products.ToList();

        }
    }
}
