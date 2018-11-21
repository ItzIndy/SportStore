using Microsoft.EntityFrameworkCore;
using SportsStore.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Data.Repositories {
    public class ProductRepository : IProductRepository{
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) {
            this._context = context;
        }

        public void Add(Product product) {
            _context.Add(product);
        }

        public void Delete(int id) {
            _context.Remove(GetBy(id));
        }

        public IEnumerable<Product> GetAll() {
            return _context.Products.OrderBy(p => p.Name).ToList();
            //Als je een lijst NIET zo willen editen dus gewoon read-only is,
            //kan je ook de optie AsNoTracking() van het entityframework.
            //return _context.Products.AsNoTracking().OrderBy(p => p.Name).ToList();
        }

        public Product GetBy(int id) {
            return _context.Products.Include(p=>p.Category).FirstOrDefault(p => p.ProductId == id);
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
