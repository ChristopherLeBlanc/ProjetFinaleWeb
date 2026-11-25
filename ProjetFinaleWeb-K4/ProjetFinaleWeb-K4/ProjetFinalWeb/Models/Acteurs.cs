using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("Acteurs")]
    public class Acteur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoActeur { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(50)]
        public string Nom { get; set; } = "";

        // F=Femme, H=Homme, N=Non connu
        [Required]
        [StringLength(1)]
        [Column(TypeName = "nchar(1)")]
        [RegularExpression("^[FHN]$", ErrorMessage = "Sexe : F, H ou N.")]
        public string Sexe { get; set; } = "N";

        public virtual ICollection<FilmActeur> FilmsActeurs { get; set; } = new List<FilmActeur>();
    }
}