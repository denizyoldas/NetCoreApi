﻿using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new EfProductDal());
            foreach (var item in productManager.GetByUnitPrice(40, 100))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(item.ProductName);
            }
        }
    }
}