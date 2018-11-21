using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Domain {
    public interface IProductRepository {

        IEnumerable<Product> GetAll();
        Product GetBy(int id);
        void Add(Product product);
        void Delete(int id);
        void SaveChanges();



    }
}
