using DotNetCoreMVCOefeningenreeks2.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMVCOefeningenreeks2.Views.Shared.Components
{
    public class ShopItemsListViewComponent: ViewComponent
    {
        private ShopItemRepository shopItemRepository;

        public ShopItemsListViewComponent(ShopItemRepository shopItemRepo)
        {
            shopItemRepository = shopItemRepo;
        }

        public IViewComponentResult Invoke(string item, int? quantity)
        {
            return View(shopItemRepository.FindShopItemList(item ?? "", quantity ?? byte.MaxValue));
        }
    }
}
