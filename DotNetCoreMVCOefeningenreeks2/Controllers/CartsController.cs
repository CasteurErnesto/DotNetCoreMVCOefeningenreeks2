using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreMVCOefeningenreeks2.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreMVCOefeningenreeks2.Controllers
{
    public class CartsController : Controller
    {
        private MyShopLiContext db;

        public CartsController(MyShopLiContext context)
        {
            db = context;
        }
       
        #region Index
        public IActionResult Index()
        {
            return View(db.Cart
                        .Select(s => s)
                        .ToList());
        }
        #endregion Index

        #region Create
        [HttpGet]
        public ViewResult Create()
        {
            return View(new Cart());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Cart.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(cart);
            }
        }
        #endregion Create


    }
}