using System;
using System.Collections.Generic;

#nullable disable

namespace EchoBot2.Models
{
    public partial class Menu
    {
        public Menu()
        {
            MenuDetails = new HashSet<MenuDetail>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsOnOrder { get; set; }
        public bool? Discontinued { get; set; }
        public string MenuDescription { get; set; }
        public string Images { get; set; }

        public virtual ICollection<MenuDetail> MenuDetails { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
