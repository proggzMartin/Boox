using System.ComponentModel.DataAnnotations;

namespace Boox.Core.Models.Entities
{
    public class Book : BookBase
    {
        [Key]
        public string Id { get; set; }
    }
}
