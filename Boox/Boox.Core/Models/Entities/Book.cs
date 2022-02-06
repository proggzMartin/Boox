using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boox.Core.Models.Entities
{
    public class Book : BookBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Unfortunately, was not successful in autogenerating
        //incrementing Id "B13", "B14" ... 
        public int Id { get; set; }
    }
}
