using System;
using System.Collections.Generic;

namespace Norwind.Models
{
    public partial class Employee : BaseModel
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
        }

        public int EmployeeId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Photo { get; set; }
        public string? Notes { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
