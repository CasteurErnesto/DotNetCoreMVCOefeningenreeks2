using System;
using System.Collections.Generic;

namespace DotNetCoreMVCOefeningenreeks2.Entities
{
    public partial class ShopItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte? Quantity { get; set; }
    }
}
