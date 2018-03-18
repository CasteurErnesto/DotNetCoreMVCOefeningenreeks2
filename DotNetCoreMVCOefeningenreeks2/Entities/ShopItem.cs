using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoreMVCOefeningenreeks2.Entities
{ 
    public partial class ShopItem
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter text for {0}")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a value for {0}")]
        [Range(minimum: 1, maximum: 5, ErrorMessage = "Please enter a value for {0} between {1} and {2}")]
        public byte? Quantity { get; set; }
        [Display(Name ="Category")]
        public int? CategoryId { get; set; }
        [Display(Name = "Cart")]
        public int? CartId { get; set; }

        public Cart Cart { get; set; } //Navigation Property

        public Category Category { get; set; } //Navigation Property
    }
}
