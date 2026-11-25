
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
        [Column("Titre")]
        public string Titre { get; set; } = "";

        [StringLength(50)]
        public string? TitreOriginal { get; set; }

        public bool? VersionEtendue { get; set; }

        [Column("UtilisateurMAJ")]
        public int? NoUtilisateurMAJ { get; set; }

        [Column("Realisateur")]
        public int? NoRealisateur { get; set; }

        [Column("Producteur")]
        public int? NoProducteur { get; set; }

        // Relations

        [ForeignKey(nameof(Categorie))]
        public virtual Categorie? CategorieInfo { get; set; }

        [ForeignKey(nameof(Format))]
        public virtual Format? FormatInfo { get; set; }

        [ForeignKey(nameof(NoProducteur))]
        public virtual Producteur? Producteur { get; set; }

        [ForeignKey(nameof(NoRealisateur))]
        public virtual Realisateur? Realisateur { get; set; }

        [ForeignKey(nameof(NoUtilisateurMAJ))]
        public virtual Utilisateur? UtilisateurMAJ { get; set; }
    }
}
