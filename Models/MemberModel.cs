using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class MemberModel
    {
        public Guid Idmember { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Adress { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;
    }
}
