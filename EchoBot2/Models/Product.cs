using System;
using System.Collections.Generic;

#nullable disable

namespace EchoBot2.Models
{
    public partial class Product
    {
        public Product()
        {
            MenuDetails = new HashSet<MenuDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsOnOrder { get; set; }
        public bool? Discontinued { get; set; }
        public int? Calories { get; set; }
        public float? Protein { get; set; }
        public float? Carb { get; set; }
        public float? Fat { get; set; }
        public string ProductDescription { get; set; }
        public string Images { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<MenuDetail> MenuDetails { get; set; }
    }
}
