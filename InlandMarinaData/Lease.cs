using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    [Table("Lease")]
    public class Lease
    {
        public int ID { get; set; }

        [Required]
        public int SlipID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public bool Active { get; set; }


        //navigation properties
        public virtual Customer Customer { get; set; }
        public virtual Slip Slip { get; set; }
    }

}
