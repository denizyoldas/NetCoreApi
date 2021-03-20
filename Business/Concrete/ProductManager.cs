using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        //[SecuredOperation("product.add, admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductCountOFCategoryCorrect(product.CategoryID),
                CheckIfProductNameExistts(product.ProductName), CheckIfCategoryLimitExceded());
            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        [CacheAspect]  // key, value
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 1)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductList);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryID == id));
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<Product> getById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductID == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            CheckIfProductCountOFCategoryCorrect(product.CategoryID);
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        private IResult CheckIfProductCountOFCategoryCorrect(int categoryId)
        {
            int productList = _productDal.GetAll(p => p.CategoryID == categoryId).Count;
            if (productList >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategory);
            }

            return new SuccessResult();
        }

        private IResult CheckIfProductNameExistts(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            if (product.UnitPrice < 10)
            {
                throw new Exception("");
            }
            Add(product);

            return null;
        }
    }
}
