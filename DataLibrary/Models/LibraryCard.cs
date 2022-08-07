using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class LibraryCard
    {
        [Key]
        public int CardID { get; set; }
        public decimal Overdue_Fee { get; set; }
        public virtual IEnumerable<LoanedAsset> LoanedAssets { get; set; }
        [Required]
        public DateTime Created_Date { get; set; }
    }
}
