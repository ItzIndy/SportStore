using SportsStore.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.ProductViewModel {
    public class EditViewModel {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }

        public EditViewModel() {
            InStock = true;
        }

        public EditViewModel(Product product) {
            this.Name = product.Name;
            this.Description = product.Description;
            this.Price = product.Price;
            this.InStock = product.InStock;
            this.CategoryId = product.Category.CategoryId;
         
            
        }

    }
}
