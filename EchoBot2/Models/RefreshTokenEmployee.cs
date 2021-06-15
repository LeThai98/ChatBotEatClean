using System;
using System.Collections.Generic;

#nullable disable

namespace EchoBot2.Models
{
    public partial class RefreshTokenEmployee
    {
        public int TokenId { get; set; }
        public int EmployeeId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
