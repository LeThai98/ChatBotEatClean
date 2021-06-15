using System;
using System.Collections.Generic;

#nullable disable

namespace EchoBot2.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        public int PaymentId { get; set; }
        public string PaymentSolution { get; set; }
        public string PayCompany { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
