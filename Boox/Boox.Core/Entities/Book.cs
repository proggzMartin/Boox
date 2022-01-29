using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Book
    {
        [Key]
        public string Id { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public DateTime Published { get; set; }
        public string Title { get; set; }
    }
}
