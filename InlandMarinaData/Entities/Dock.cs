using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InlandMarinaData.Entities
{
    [Table("dock")]  
    public class Dock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("waterservice")]
        public bool WaterService { get; set; }

        [Column("electricalservice")]
        public bool ElectricalService { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // navigation property
        public virtual ICollection<Slip> Slips { get; set; } = new List<Slip>();
    }
}