using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class LibraryBranch
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Branch Name should nor exceed 30 characters.")]
        public string Branch_Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Telephone_Number { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime Date_Founded { get; set; }
        public string ImageUrl { get; set; }
        public virtual IEnumerable<Patron> Branch_Patrons { get; set; }
        public virtual IEnumerable<LibraryAsset> Branch_Assets { get; set; }
        //public LibraryAsset Asset_Value { get; set; }

    }
}
