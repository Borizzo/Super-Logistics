using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace SuperLogistics.Models
{
    public class Product
    {
        [Required(ErrorMessage = "This Field Can't be empty")]
        [StringLength(8, MinimumLength =8, ErrorMessage ="EAN must Have 8 Numbers")]
        public String EAN { get; set; }
       
        [Required(ErrorMessage ="This Field Can't be empty")]

        [StringLength(50, MinimumLength = 3,  ErrorMessage = "Name must have 3 to 50 signs")]
        public String Name { get; set; }

        public int Stock { get; set; }

       
        [Column(TypeName = "decimal(16, 2)")]
        [DataType(DataType.Currency)]
        public Decimal Value { get; set; }

        [Display(Name = "Storage Placement")]
        public String StoragePlacement { get; set; }
    }
}
