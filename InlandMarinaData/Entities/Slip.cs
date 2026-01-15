using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InlandMarinaData.Entities
{
    [Table("slip")] 
    public class Slip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID { get; set; }

        [Required]
        [Column("width")]
        public int Width { get; set; }

        [Required]
        [Column("length")]
        public int Length { get; set; }

        [Required]
        [Column("dockid")]
        public int DockID { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // navigation properties
        [ForeignKey("DockID")]
        public virtual Dock Dock { get; set; } = null!;

        public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();
    }
}