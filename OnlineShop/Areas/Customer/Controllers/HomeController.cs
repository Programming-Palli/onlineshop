﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Utility;

namespace OnlineShop.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

       
        public IActionResult Index()
        {
            return View(_db.Products.Include(c=>c.ProductTypes).Include(c=>c.SpecialTag).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        //GET product detail acation method

        public ActionResult Detail(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if(product==null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST product detail acation method
        [HttpPost]
        [ActionName("Detail")]
        public ActionResult ProductDetail(int? id)
        {
            List<Products>products=new List<Products>();
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            products = HttpContext.Session.Get<List<Products>>("products");
            if(products==null)
            {
                products=new List<Products>();
            }
            products.Add(product);
            HttpContext.Session.Set("products", products);
            return View(product);
        }


    }
}
