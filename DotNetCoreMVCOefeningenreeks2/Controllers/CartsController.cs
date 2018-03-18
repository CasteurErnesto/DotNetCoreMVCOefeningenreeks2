﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreMVCOefeningenreeks2.Entities;
using DotNetCoreMVCOefeningenreeks2.Models;
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
                        .Select(c => c)
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

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                Cart cartToEdit = db.Cart
                        .Where(c => c.Id == id)
                        .Select(c => c)
                        .SingleOrDefault();
                if (cartToEdit != null)
                {
                    return View(cartToEdit);
                }
            }
            return View("Error", new ErrorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Cart.Update(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cart);
        }
        #endregion Edit

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                Cart cartToDelete = db.Cart
                      .Where(c => c.Id == id)
                      .Select(c => c)
                      .SingleOrDefault();
                if (cartToDelete != null)
                {
                    db.Cart.Remove(cartToDelete);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        #endregion Delete
    }
}