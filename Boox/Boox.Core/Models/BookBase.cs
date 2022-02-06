namespace Boox.Core.Models
{
    //Class properties in common for Book
    //and BookDto. To avoid Copypaste, uses
    //this as parentclass for both Dto and entity.
    //Abstract since children should be used instead.
    //Property Published is virtual for BookDto to
    //override and combine with JsonPropertyName-attribute.
    public abstract class BookBase
    {
        public string Author { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public virtual DateTime Published { get; set; }
        public string Title { get; set; }
    }
}
