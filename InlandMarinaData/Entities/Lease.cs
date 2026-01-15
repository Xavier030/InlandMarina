using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InlandMarinaData.Entities
{
    [Table("lease")] 
    public class Lease
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID { get; set; }

        [Required]
        [Column("slipid")]
        public int SlipID { get; set; }

        [Required]
        [Column("customerid")]
        public int CustomerID { get; set; }

        [Column("active")]
        public bool Active { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("startdate")]
        public DateTime? StartDate { get; set; }

        [Column("enddate")]
        public DateTime? EndDate { get; set; }

        // navigation properties
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; } = null!;

        [ForeignKey("SlipID")]
        public virtual Slip Slip { get; set; } = null!;
    }
}