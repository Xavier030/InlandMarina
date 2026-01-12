using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    [Table("Customer")]
    public class Customer
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30)]
        public string City { get; set; }


        [Required]
        [StringLength(30)]
        public string Username { get; set; } 

        [Required]
        [StringLength(30)]
        public string Password { get; set; }

        [RegularExpression(@"^$\d{3}$\s\d{3}-\d{4}$", ErrorMessage = "Phone format: (XXX) XXX-XXXX")]
        public string Phone { get; set; }

        // navigation property
        public virtual ICollection<Lease> Leases { get; set; }
    }

}
