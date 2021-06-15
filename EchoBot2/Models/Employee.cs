using System;
using System.Collections.Generic;

#nullable disable

namespace EchoBot2.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
            RefreshTokenEmployees = new HashSet<RefreshTokenEmployee>();
        }

        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<RefreshTokenEmployee> RefreshTokenEmployees { get; set; }
    }
}
