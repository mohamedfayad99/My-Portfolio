using System.ComponentModel.DataAnnotations.Schema;

namespace books.Models
{
    public class Owner 
    {
        public int Id { get; set; }
        public string Name   { get; set; }
        public string Profile { get; set; }
        public string Avatar { get; set; }


    }
}
