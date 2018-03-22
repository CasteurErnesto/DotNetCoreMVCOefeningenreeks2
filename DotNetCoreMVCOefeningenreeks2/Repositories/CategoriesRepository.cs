using DotNetCoreMVCOefeningenreeks2.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMVCOefeningenreeks2.Repositories
{
    public class CategoriesRepository
    {
        private MyShopLiContext db;

        public CategoriesRepository(MyShopLiContext context)
        {
            db = context;
        }

        public Category GetCategory(int id)
        {
            return db.Category
                     .SingleOrDefault(c => c.Id == id);
        }

        public List<Category> GetCategoryList()
        {
            return db.Category
                        .Select(c => c)
                        .ToList();
        }

        public bool CreateCategory(Category category)
        {
            db.Category.Add(category);
            db.SaveChanges();

            return db.Category.SingleOrDefault(c => c.Id == category.Id) != null;
        }

        public bool UpdateCategory(Category category)
        {
            db.Category.Update(category);
            db.SaveChanges();
            
            return db.Category.SingleOrDefault(c => c.Id == category.Id) != null;
        }

        public bool RemoveCategory(Category category)
        {
            db.Category.Remove(category);
            db.SaveChanges();

            return db.Category.SingleOrDefault(c => c.Id == category.Id) is null;
        }

    }
}
