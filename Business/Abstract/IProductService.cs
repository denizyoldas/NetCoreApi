using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        List<Product> getAll();
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        List<Product> GetAllByCategory(int categoryId);
    }
}
