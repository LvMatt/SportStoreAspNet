using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class Staff
    {
        public int StaffId { get; set; }
        public string StaffFullname { get; set; }
        public string StaffPhone { get; set; }
        public string StaffPosition { get; set; }
        public string StaffAddress { get; set; }
        public int BranchesBranchesId { get; set; }

        public virtual Branches BranchesBranches { get; set; }
    }
}
