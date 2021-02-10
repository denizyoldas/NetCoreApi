using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {

        }

        public void Delete(Product product)
        {

        }

        public List<Product> getAll()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _productDal.GetAll();
        }

        public void Update(Product product)
        {

        }
    }
}
