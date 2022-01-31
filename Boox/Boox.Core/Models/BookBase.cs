using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boox.Core.Models
{
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
