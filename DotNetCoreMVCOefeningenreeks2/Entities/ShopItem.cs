using System;
using System.Collections.Generic;

namespace DotNetCoreMVCOefeningenreeks2.Entities
{
    public partial class ShopItem
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public byte? Quantity { get; set; }
    }
}
