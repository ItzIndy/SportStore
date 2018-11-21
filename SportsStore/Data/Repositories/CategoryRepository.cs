using Microsoft.EntityFrameworkCore;
using SportsStore.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Data.Repositories {
    public class CategoryRepository : ICategoryRepository {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) {
            this._context = context;
        }
        public IEnumerable<Category> GetAll() {
            return _context.Categories.Include(c=>c.Products).OrderBy(c => c.Name);
        }

        public Category GetById(int id) {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }
    }
}
