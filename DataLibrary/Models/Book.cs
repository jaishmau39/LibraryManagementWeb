using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Book : LibraryAsset
    {
        [Required]
        public string Author { get; set; }
        [Required]
        public string DeweyIndex{ get; set; }
        [Required]
        public string ISBN { get; set; }
    }
}
