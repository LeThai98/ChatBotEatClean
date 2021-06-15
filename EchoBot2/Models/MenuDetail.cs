using System;
using System.Collections.Generic;

#nullable disable

namespace EchoBot2.Models
{
    public partial class MenuDetail
    {
        public int ProductId { get; set; }
        public int MenuId { get; set; }
        public short? Quantity { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Product Product { get; set; }
    }
}
