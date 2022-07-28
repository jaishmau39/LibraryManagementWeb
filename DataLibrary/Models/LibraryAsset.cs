using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public abstract class LibraryAsset
    {
        [Key]
        public int Asset_ID { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Cost { get; set; }
        //public decimal Replacement_Cost { get; set; }
        public int NumberofCopies { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public AssetStatus Availability { get; set; }
        public virtual LibraryBranch Last_Location { get; set; }

    }
}
