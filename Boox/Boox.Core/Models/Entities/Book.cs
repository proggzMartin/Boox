using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boox.Core.Models.Entities
{
    public class Book : BookBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Unsuccessful in attempts to autogenerate ID "B13", "B14" ... 
        public string Id { get; set; }
    }
}
