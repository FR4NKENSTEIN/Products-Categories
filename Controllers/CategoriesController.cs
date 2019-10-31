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
    public class CategoriesController : Controller
    {
        private MyContext database;
        public CategoriesController(MyContext context)
        {
            database = context;
        }
        [HttpGet("categories")]
        public IActionResult CatDisplay()
        {
             List<Category> myCategories = database.Categories.ToList();
            ViewBag.Items = myCategories;
            return View();
        }

        [HttpPost("categories/create")]
        public IActionResult CatCreator(Category newCat)
        {
            if (ModelState.IsValid)
            {
                database.Add(newCat);
                database.SaveChanges();
                return RedirectToAction("CatDisplay");
            }
            return View(newCat);
        }

        [HttpGet("categories/{catId:int}")]
        public IActionResult CatDetail(int catId)
        {
            Category ThisCategory = database.Categories
                .Include(c => c.OfProducts)
                .ThenInclude(x=> x.Product)
                .FirstOrDefault(c => c.CategoryId == catId);
            List<Product> ProToAdd = database.Products
                .Where(p => p.OfCategories
                .All(c => c.CategoryId != ThisCategory.CategoryId))
                .ToList();
            ViewBag.Cat = ThisCategory;
            ViewBag.ProsAvailable = ProToAdd;
            return View();
        }

        [HttpPost("categories/add")]
        public IActionResult CatAdder(Association link)
        {
            database.Associations.Add(link);
            database.SaveChanges();
            return RedirectToAction("CatDetail", new{catId = link.CategoryId});
        }
    }
}
