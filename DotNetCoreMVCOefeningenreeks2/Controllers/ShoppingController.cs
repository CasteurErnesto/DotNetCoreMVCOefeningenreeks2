using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreMVCOefeningenreeks2.Entities;
using DotNetCoreMVCOefeningenreeks2.Models;
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

        #region Index
        public IActionResult Index()
        {
            return View(db.ShopItem
                    .FromSql($"Select * from ShopItem")
                    .ToList());
        }
        #endregion Index

        #region Create
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
                db.Database
                       .ExecuteSqlCommand($"Insert Into ShopItem (Item, Quantity) " +
                                $"Values ('{shopItem.Item}', '{shopItem.Quantity}')");

                return RedirectToAction("Index");
            }
            else
            {
                return View(shopItem);
            }
        }
        #endregion Create

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                ShopItem shopItemToEdit = db.ShopItem
                        .FromSql($"Select * from ShopItem where Id = {id}")
                        .SingleOrDefault();
                if (shopItemToEdit != null)
                {
                    return View(shopItemToEdit);
                }
            } 
            return View("Error", new ErrorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ShopItem shopItem)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand($"Update ShopItem " +
                                             $"Set Item = '{shopItem.Item}', Quantity = '{shopItem.Quantity}' " +
                                              $"Where Id = {shopItem.Id} ");
                return RedirectToAction("Index");
            }
            return View(shopItem);
        }
        #endregion Edit

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id != null) {
                ShopItem shopItemToDelete = db.ShopItem
                    .FromSql($"Select * from ShopItem where Id = {id}")
                    .SingleOrDefault();
                if(shopItemToDelete != null)
                {
                    db.Database
                     .ExecuteSqlCommand($"Delete From ShopItem " +
                              $"Where Id = {id} ");
                }
            }
            return RedirectToAction("Index");
        }
        #endregion Delete

        #region Find
        public ViewResult Find(string item, int? aantal)
        {
            return View("Index", 
                  db.ShopItem.FromSql(
                      $"Select * from ShopItem " +
                      $"where item like '{item ?? "%"}%' " +
                      $"and Quantity <= {aantal ?? byte.MaxValue}")
                  .ToList());
        }
        #endregion Find
    }
}