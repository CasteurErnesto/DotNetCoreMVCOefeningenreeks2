using DotNetCoreMVCOefeningenreeks2.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMVCOefeningenreeks2.Repositories
{
    public class CartRepository
    {
        private MyShopLiContext db;

        public CartRepository(MyShopLiContext context)
        {
            db = context;
        }

        public Cart GetCart(int id)
        {
            return db.Cart
                     .SingleOrDefault(c => c.Id == id);
        }

        public List<Cart> GetCartList()
        {
            return db.Cart
                        .Select(c => c)
                        .ToList();
        }

        public bool CreateCart(Cart cart)
        {
            db.Cart.Add(cart);
            db.SaveChanges();

            return db.Cart.SingleOrDefault(c => c.Id == cart.Id) != null;
        }

        public bool UpdateCart(Cart cart)
        {
            db.Cart.Update(cart);
            db.SaveChanges();
            
            return db.Cart.SingleOrDefault(c => c.Id == cart.Id) != null;
        }

        public bool RemoveCart(Cart cart)
        {
            db.Cart.Remove(cart);
            db.SaveChanges();

            return db.Cart.SingleOrDefault(c => c.Id == cart.Id) is null;
        }

    }
}
