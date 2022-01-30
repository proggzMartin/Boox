namespace Boox.Core.Models
{
    /// <summary>
    /// Purpose of class - decrease amount of copypasted
    /// properties when creating BookDto.
    /// Abstract - should always be iherited by using class.
    /// </summary>
    public abstract class BookBase
    {
        public string Author { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public DateTime Published { get; set; }
        public string Title { get; set; }
    }
}
