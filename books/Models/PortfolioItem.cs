using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace books.Models
{
    public class PortfolioItem 
    {
        [Key]
        public int Id { get; set; }
        public string ProjectNAme { get; set; }
        public string Description { get; set; }
        public string? ImageURL { get; set; }

        [NotMapped]

        public IFormFile File { get; set; }
        [NotMapped]
        public IFormFile? UpdateFile { get; set; }

    }
}
