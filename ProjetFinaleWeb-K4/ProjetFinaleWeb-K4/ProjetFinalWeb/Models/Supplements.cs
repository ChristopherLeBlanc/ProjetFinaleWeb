using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("Supplements")]
    public class Supplement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoSupplement { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; } = "";

        public virtual ICollection<FilmSupplement> FilmsSupplements { get; set; } = new List<FilmSupplement>();
    }
}