using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoreMVCOefeningenreeks2.Entities
{
    public partial class Category
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ShopItem> ShopItem { get; set; }
    }
}
