using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class BranchTimes
    {
        [Key]
        public int ID { get; set; }
        public LibraryBranch LibraryBranch { get; set; }
        [Range(0,6)]
        public int Day_of_Week { get; set; }
        [Range(0,23)]
        public int OpeningTime { get; set; }
        [Range(0,23)]
        public int ClosingTime { get; set; }
    }
}
