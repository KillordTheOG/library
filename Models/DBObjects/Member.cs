using System;
using System.Collections.Generic;

namespace Library.Models.DBObjects
{
    public partial class Member
    {
        public Member()
        {
            Loans = new HashSet<Loan>();
        }

        public Guid Idmember { get; set; }
        public string Name { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual ICollection<Loan> Loans { get; set; }
    }
}
