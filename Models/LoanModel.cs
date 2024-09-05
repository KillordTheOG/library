namespace Library.Models
{
    public class LoanModel
    {
        public Guid Idmember { get; set; }
        public Guid Idbook { get; set; }
        public Guid Idloan { get; set; }

        public MemberModel Member { get; set; }

        public BookModel Book { get; set; }
    }
}
