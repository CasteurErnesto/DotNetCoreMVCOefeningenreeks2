using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoreMVCOefeningenreeks2.Entities
{
    public enum Suggestion
    {
        Chocolade = 1,
        Yoghurt,
        Melk,
        Chips,
        Koffie
    }
    public partial class ShopItem
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter text for {0}")]
        public string Item { get; set; }
        [Required(ErrorMessage = "Please enter a value for {0}")]
        [Range(minimum:1, maximum:5, ErrorMessage ="Please enter a value for {0} between {1} and {2}")]
        public byte Quantity { get; set; }
    }
}
