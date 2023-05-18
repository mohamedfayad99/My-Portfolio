using System.ComponentModel.DataAnnotations;

namespace books.Models
{
    public class Contactme
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string? Messsage { get; set; }
    }
}
