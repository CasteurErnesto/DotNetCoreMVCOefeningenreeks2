using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoreMVCOefeningenreeks2.Entities
{
    public partial class ShopItem
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string Item { get; set; }
        public byte Quantity { get; set; }
    }
}
