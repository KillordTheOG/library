using System;
using System.Collections.Generic;

namespace Library.Models.DBObjects
{
    public partial class Book
    {
        public Book()
        {
            Loans = new HashSet<Loan>();
        }

        public Guid Idbook { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
    }
}
