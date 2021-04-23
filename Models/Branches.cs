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

        public int BranchesId { get; set; }
        public string BranchesAddress { get; set; }
        public string BranchesPhone { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
