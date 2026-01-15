using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InlandMarinaData.Entities
{
    [Table("customer")]  
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        [Column("firstname")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        [Column("lastname")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        [Column("phone")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        [Column("city")]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // navigation property
        public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();
    }
}