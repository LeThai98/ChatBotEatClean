using System;
using System.Collections.Generic;

#nullable disable

namespace EchoBot2.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string District { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
