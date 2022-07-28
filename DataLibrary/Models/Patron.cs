using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Patron
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }

        public virtual LibraryCard Library_Card { get; set; }
        public virtual LibraryBranch Branch_Location { get; set; }
    }
}
