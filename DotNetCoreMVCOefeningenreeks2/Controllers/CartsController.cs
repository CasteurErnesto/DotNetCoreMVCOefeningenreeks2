using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreMVCOefeningenreeks2.Entities;
using DotNetCoreMVCOefeningenreeks2.Models;
using DotNetCoreMVCOefeningenreeks2.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreMVCOefeningenreeks2.Controllers
{
    public class CartsController : Controller
    {
        private CartRepository cartRepository;

        public CartsController(CartRepository repo)
        {
            cartRepository = repo;
        }
       
        #region Index
        public IActionResult Index()
        {
            return View(cartRepository.GetCartList());
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
                cartRepository.CreateCart(cart);
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
                Cart cartToEdit = cartRepository.GetCart((int)id);
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
                cartRepository.UpdateCart(cart);
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
                Cart cartToDelete = cartRepository.GetCart((int)id);
                if (cartToDelete != null)
                {
                    cartRepository.RemoveCart(cartToDelete);
                }
            }
            return RedirectToAction("Index");
        }
        #endregion Delete
    }
}