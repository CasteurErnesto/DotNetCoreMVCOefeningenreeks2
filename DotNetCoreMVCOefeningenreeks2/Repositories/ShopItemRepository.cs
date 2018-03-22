using DotNetCoreMVCOefeningenreeks2.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMVCOefeningenreeks2.Repositories
{
    public class ShopItemRepository
    {
        private MyShopLiContext db;

        public ShopItemRepository(MyShopLiContext context)
        {
            db = context;
        }

        public ShopItem GetShopItem(int id)
        {
            return db.ShopItem
                     .SingleOrDefault(s => s.Id == id);
        }

        public List<ShopItem> GetShopItemList()
        {
            return db.ShopItem
                        .Include("Cart") //eager loading
                        .Include("Category") //eager loading
                        .Select(s => s)
                        .ToList();
        }

        public bool CreateShopItem(ShopItem shopItem)
        {
            db.ShopItem.Add(shopItem);
            db.SaveChanges();

            return db.ShopItem.SingleOrDefault(s => s.Id == shopItem.Id) != null;
        }

        public bool UpdateShopItem(ShopItem shopItem)
        {
            db.ShopItem.Update(shopItem);
            db.SaveChanges();
            
            return db.ShopItem.SingleOrDefault(s => s.Id == shopItem.Id) != null;
        }

        public bool RemoveShopItem(ShopItem shopItem)
        {
            db.ShopItem.Remove(shopItem);
            db.SaveChanges();

            return db.ShopItem.SingleOrDefault(s => s.Id == shopItem.Id) is null;
        }

        public List<ShopItem> FindShopItemList(string search, int quantity)
        {
            return db.ShopItem
                 .Where(s => s.Name.StartsWith(search) && s.Quantity <= quantity)
                 .Select(s => s)
                 .ToList();
        }

    }
}
