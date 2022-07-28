using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class AssetStatus
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
