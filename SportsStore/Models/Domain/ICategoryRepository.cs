using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Domain {
    public interface ICategoryRepository {
        IEnumerable<Category> GetAll();
        Category GetById(int id);

    }
}
