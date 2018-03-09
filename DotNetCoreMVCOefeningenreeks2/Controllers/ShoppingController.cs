using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreMVCOefeningenreeks2.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreMVCOefeningenreeks2.Controllers
{
    //Scaffold-DbContext -Connection "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyShopLi;Integrated Security=True" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Context MyShopLiContext

    public class ShoppingController : Controller
    {
        MyShopLiContext db;
        public ShoppingController()
        {
            db = new MyShopLiContext();
        }

        public IActionResult Index()
        {
            return View(db.ShopItem
                    .FromSql($"Select * from ShopItem")
                    .ToList());
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View(new ShopItem());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ShopItem shopItem)
        {
            if (ModelState.IsValid)
            {
                db.ShopItem.Add(shopItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(shopItem);
            }
        }

       [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                ShopItem shopItemToEdit = db.ShopItem
                        .FromSql($"Select * from ShopItem where Id = {id}")
                        .SingleOrDefault();
                return View(shopItemToEdit);
            } else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ShopItem shopItem)
        {
            if (ModelState.IsValid)
            {
                db.ShopItem.Update(shopItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shopItem);
        }

        public IActionResult Delete(int? id)
        {
            if (id != null) {
                ShopItem shopItemToDelete = db.ShopItem
                    .FromSql($"Select * from ShopItem where Id = {id}")
                    .SingleOrDefault();
                if(shopItemToDelete != null)
                {
                    db.ShopItem.Remove(shopItemToDelete);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }


        public ViewResult Find(string item, int? aantal)
        {
            return View("Index", 
                  db.ShopItem.FromSql(
                      $"Select * from ShopItem " +
                      $"where item like '{item ?? "%"}%' " +
                      $"and Quantity <= {aantal ?? byte.MaxValue}")
                  .ToList());
        }
    }
}