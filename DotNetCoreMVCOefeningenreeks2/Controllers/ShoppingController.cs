using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreMVCOefeningenreeks2.Common;
using DotNetCoreMVCOefeningenreeks2.Entities;
using DotNetCoreMVCOefeningenreeks2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using DotNetCoreMVCOefeningenreeks2.Repositories;

namespace DotNetCoreMVCOefeningenreeks2.Controllers
{
    //Scaffold-DbContext -Connection "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyShopLi;Integrated Security=True" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Context MyShopLiContext
    // Use the extra option -f to 'force' overwriting the current entities in your project
    public class ShoppingController : Controller
    {
        private ShopItemRepository shopItemRepository;
        private CategoriesRepository categoriesRepository;
        private CartRepository cartRepository;

        public ShoppingController(ShopItemRepository shopItemRepo, 
                                    CategoriesRepository catRepo,
                                       CartRepository cartRepo)
        {
            shopItemRepository = shopItemRepo;
            categoriesRepository = catRepo;
            cartRepository = cartRepo;
        }

        #region Index
        public IActionResult Index()
        {
            return View(shopItemRepository.GetShopItemList());
        }
        #endregion Index

        #region Create
        [HttpGet]
        public ViewResult Create()
        {
            ViewBag.Suggestion = (Suggestion)
                                    new Random()
                                    .Next(1, (Enum.GetValues(typeof(Suggestion)).Length) + 1);

            ViewBag.CartId = cartRepository.GetCartList()
                                .OrderBy(c => c.Name)
                                .Select(c => new SelectListItem()
                                {
                                    Text = c.Name,
                                    Value = c.Id.ToString()
                                }).ToList();
            ViewBag.CategoryId = categoriesRepository.GetCategoryList()
                                  .OrderBy(c => c.Name)
                                  .Select(c => new SelectListItem()
                                  {
                                      Text = c.Name,
                                      Value = c.Id.ToString()
                                  }).ToList();
            return View(new ShopItem());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ShopItem shopItem)
        {
            if (ModelState.IsValid)
            {
                shopItemRepository.CreateShopItem(shopItem);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CartId = cartRepository.GetCartList().OrderBy(c => c.Name).Select(c => new SelectListItem(){Text = c.Name,Value = c.Id.ToString()}).ToList();
                ViewBag.CategoryId = categoriesRepository.GetCategoryList().OrderBy(c => c.Name).Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() }).ToList();
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
                ShopItem shopItemToEdit = shopItemRepository.GetShopItem((int)id);
                if (shopItemToEdit != null)
                {
                    ViewBag.CartId = cartRepository.GetCartList().OrderBy(c => c.Name).Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() }).ToList();
                    ViewBag.CategoryId = categoriesRepository.GetCategoryList().OrderBy(c => c.Name).Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() }).ToList();
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
                shopItemRepository.UpdateShopItem(shopItem);
                return RedirectToAction("Index");
            }
            ViewBag.CartId = cartRepository.GetCartList().OrderBy(c => c.Name).Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() }).ToList();
            ViewBag.CategoryId = categoriesRepository.GetCategoryList().OrderBy(c => c.Name).Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() }).ToList();
            return View(shopItem);
        }
        #endregion Edit

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                ShopItem shopItemToDelete = shopItemRepository.GetShopItem((int)id);
                if (shopItemToDelete != null)
                {
                    shopItemRepository.RemoveShopItem(shopItemToDelete);
                }
            }
            return RedirectToAction("Index");
        }
        #endregion Delete

        #region Find
        public ViewResult Find(string item, int? quantity)
        {
            return View("Index",
                  shopItemRepository.FindShopItemList(item ?? "", quantity ?? byte.MaxValue));
        }
        #endregion Find

    }
}