using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;

        public InMemoryProductDal()
        {
            _products = new List<Product> {
                new Product{ ProdductId=1, CategoryId = 1, ProductName = "Bardak", UnitPrice = 15, UnitInStok = 15 },
                new Product{ ProdductId=3, CategoryId = 1, ProductName = "Kamera", UnitPrice = 500, UnitInStok = 3 },
                new Product{ ProdductId=4, CategoryId = 2, ProductName = "Telefon", UnitPrice = 15, UnitInStok = 15 },
                new Product{ ProdductId=5, CategoryId = 2, ProductName = "Klavye", UnitPrice = 150, UnitInStok = 65 },
                new Product{ ProdductId=5, CategoryId = 2, ProductName = "Fare", UnitPrice = 85, UnitInStok = 1 },
            };
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            Product productToDelete = _products.SingleOrDefault(p => p.ProdductId == product.ProdductId);
            _products.Remove(productToDelete);
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public void Update(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p => p.ProdductId == product.ProdductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitInStok = product.UnitInStok;
            productToUpdate.UnitPrice = product.UnitPrice;
        }
    }
}
