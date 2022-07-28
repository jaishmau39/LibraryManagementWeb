using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Hold
    {
        [Key]
        public int ID { get; set; }
        public virtual LibraryCard LibraryCard { get; set; }
        public virtual LibraryAsset LibraryAsset { get; set; }
        public DateTime Hold_Placed { get; set; }
    }
}
