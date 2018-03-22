using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreMVCOefeningenreeks2.Entities;
using DotNetCoreMVCOefeningenreeks2.Models;
using DotNetCoreMVCOefeningenreeks2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreMVCOefeningenreeks2.Controllers
{
    public class CategoriesController : Controller
    {
        private CategoriesRepository categoriesRepository;

        public CategoriesController(CategoriesRepository repo)
        {
            categoriesRepository = repo;
        }

        #region Index
        public IActionResult Index()
        {
            return View(categoriesRepository.GetCategoryList());
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
                categoriesRepository.CreateCategory(category);
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
                Category categoryEdit = categoriesRepository.GetCategory((int)id);
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
                categoriesRepository.UpdateCategory(category);
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
                Category categoryToDelete = categoriesRepository.GetCategory((int)id);
                if (categoryToDelete != null)
                {
                    categoriesRepository.RemoveCategory(categoryToDelete);
                }
            }
            return RedirectToAction("Index");
        }
        #endregion Delete
    }
}