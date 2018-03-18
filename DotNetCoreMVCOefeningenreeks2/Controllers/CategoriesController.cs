using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreMVCOefeningenreeks2.Entities;
using DotNetCoreMVCOefeningenreeks2.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreMVCOefeningenreeks2.Controllers
{
    public class CategoriesController : Controller
    {
        private MyShopLiContext db;

        public CategoriesController(MyShopLiContext context)
        {
            db = context;
        }

        #region Index
        public IActionResult Index()
        {
            return View(db.Category
                        .Select(c => c)
                        .ToList());
        }
        #endregion Index

        #region Create
        [HttpGet]
        public ViewResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Category.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }
        #endregion Create

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                Category categoryEdit = db.Category
                        .Where(c => c.Id == id)
                        .Select(c => c)
                        .SingleOrDefault();
                if (categoryEdit != null)
                {
                    return View(categoryEdit);
                }
            }
            return View("Error", new ErrorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Category.Update(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        #endregion Edit

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                Category categoryToDelete = db.Category
                      .Where(c => c.Id == id)
                      .Select(c => c)
                      .SingleOrDefault();
                if (categoryToDelete != null)
                {
                    db.Category.Remove(categoryToDelete);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        #endregion Delete
    }
}