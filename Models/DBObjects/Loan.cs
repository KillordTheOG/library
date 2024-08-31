using System;
using System.Collections.Generic;

namespace Library.Models.DBObjects
{
    public partial class Loan
    {
        public Guid Idmember { get; set; }
        public Guid Idbook { get; set; }
        public Guid Idloan { get; set; }

        public virtual Book IdbookNavigation { get; set; } = null!;
        public virtual Member IdmemberNavigation { get; set; } = null!;
    }
}
