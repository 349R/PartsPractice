using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartsPractice.Models
{
    public class Parts
    {   [Key]
        public long PartId { get; set; }                    // Unique Key

        public string Name { get; set; }

        public string Description { get; set; }

        public int QtyOnHand { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public bool Available { get; set; }
    }
}
