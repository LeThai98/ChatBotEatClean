using System;
using System.Collections.Generic;

#nullable disable

namespace EchoBot2.Models
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public int MenuId { get; set; }
        public short? Quantity { get; set; }
        public float? Discount { get; set; }
        public decimal? UnitPrice { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Order Order { get; set; }
    }
}
