using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("Films")]
    public class Film
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoFilm { get; set; }

        [Range(1000, 9999)]
        public int? AnneeSortie { get; set; }

        public int? Categorie { get; set; }
        public int? Format { get; set; }
        public DateTime? DateMAJ { get; set; }
        public int? NoUtilisateurMAJ { get; set; }

        [StringLength(500)]
        public string? Resume { get; set; }

        [Range(1, 9999)]
        public int? DureeMinutes { get; set; }

        public bool? FilmOriginal { get; set; }

        [StringLength(50)]
        public string? ImagePochette { get; set; }

        [Range(1, 99)]
        public int? NbDisques { get; set; }

        [Required(ErrorMessage = "Le titre français est obligatoire.")]
        [StringLength(50)]
        public string TitreFrancais { get; set; } = "";

        [StringLength(50)]
        public string? TitreOriginal { get; set; }

        public bool? VersionEtendue { get; set; }
        public int? NoRealisateur { get; set; }
        public int? NoProducteur { get; set; }

        [Column("Xtra")]
        [StringLength(255)]
        public string? Xtra { get; set; }

        public Categorie? CategorieInfo { get; set; }
        public Format? FormatInfo { get; set; }
        public Realisateur? Realisateur { get; set; }
        public Producteur? Producteur { get; set; }
        public Utilisateur? UtilisateurMAJ { get; set; }
    }
}
