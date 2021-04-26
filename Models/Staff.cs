using System;
using System.Collections.Generic;

namespace SportStore.Model
{
    public partial class Staff
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public int BranchesId { get; set; }

        public virtual Branches Branches { get; set; }
    }
}
