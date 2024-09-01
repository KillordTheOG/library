namespace Library.Models
{
    public class MemberModel
    {
        public Guid Idmember { get; set; }
        public string Name { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
