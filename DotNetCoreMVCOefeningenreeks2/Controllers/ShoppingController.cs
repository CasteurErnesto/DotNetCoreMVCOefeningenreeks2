using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreMVCOefeningenreeks2.Entities;

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
            return View(db.ShopItem.ToList());
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View(new ShopItem());
        }

        [HttpPost]
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

        public ViewResult Filter(string keyword)
        {
            return View();
            //return View("Index", db.Maaltijd.FromSql($"Select * from Maaltijd  where Type = {keyword} ").ToList());
        }
    }
}