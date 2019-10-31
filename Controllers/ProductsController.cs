using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsCategories.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProductsCategories.Controllers
{
    public class ProductsController : Controller
    {
        private MyContext database;
        public ProductsController(MyContext context)
        {
            database = context;
        }
        [HttpGet("products")]
        public IActionResult ProDisplay()
        {
            List<Product> myProducts = database.Products.ToList();
            ViewBag.Items = myProducts;
            return View();
        }

        [HttpPost("products/create")]
        public IActionResult ProCreator(Product newPro)
        {
            if (ModelState.IsValid)
            {
                database.Add(newPro);
                database.SaveChanges();
                return RedirectToAction("ProDisplay");
            }
            return View(newPro);
        }

        [HttpGet("products/{proId:int}")]
        public IActionResult ProDetail(int proId)
        {
            Product ThisProduct = database.Products
                .Include(p => p.OfCategories)
                .ThenInclude(x=> x.Category)
                .FirstOrDefault(p => p.ProductId == proId);
            List<Category> CatToAdd = database.Categories
                .Where(c => c.OfProducts
                .All(p => p.ProductId != ThisProduct.ProductId))
                .ToList();
            ViewBag.Pro = ThisProduct;
            ViewBag.CatsAvailable = CatToAdd;
            return View();
        }

        [HttpPost("products/add")]
        public IActionResult ProAdder(Association link)
        {
            database.Associations.Add(link);
            database.SaveChanges();
            return RedirectToAction("ProDetail", new{proId = link.ProductId});
        }
    }
}