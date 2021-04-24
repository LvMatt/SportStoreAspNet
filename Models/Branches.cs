using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class Branches
    {
        public Branches()
        {
            Orders = new HashSet<Orders>();
            Staff = new HashSet<Staff>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
