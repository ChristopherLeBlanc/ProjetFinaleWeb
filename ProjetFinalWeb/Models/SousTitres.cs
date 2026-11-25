using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("SousTitres")]
    public class SousTitre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoSousTitre { get; set; }

        [Required]
        [StringLength(10)]
        public string LangueSousTitre { get; set; } = "";

        public virtual ICollection<FilmSousTitre> FilmsSousTitres { get; set; } = new List<FilmSousTitre>();
    }
}