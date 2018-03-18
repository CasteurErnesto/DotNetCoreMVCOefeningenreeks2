using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoreMVCOefeningenreeks2.Entities
{
    public partial class ShopItem
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public byte? Quantity { get; set; }
        [Display(Name ="Category")]
        public int? CategoryId { get; set; }
        [Display(Name = "Category")]
        public int? CartId { get; set; }

        public Cart Cart { get; set; }

        public Category Category { get; set; }
    }
}
