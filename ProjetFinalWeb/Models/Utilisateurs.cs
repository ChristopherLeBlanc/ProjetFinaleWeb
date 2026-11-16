using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjetFinalWeb.Models
{
    [Table("Utilisateurs")]
    public class Utilisateur 
    {
        
        [Key]
        [Column("NoUtilisateur")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoUtilisateur { get; set; }

        [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire.")]
        [StringLength(10, MinimumLength = 2,
            ErrorMessage = "Le nom d'utilisateur doit contenir entre {2} et {1} caractères.")]
        [RegularExpression(@"^[A-Za-z][A-Za-z-]*[A-Za-z]$",
            ErrorMessage = "Le nom doit débuter et se terminer par une lettre, avec seulement des lettres ou des tirets entre les deux.")]
        [Display(Name = "Nom d'utilisateur")]
        public string NomUtilisateur { get; set; } = "";

        [Required(ErrorMessage = "Le courriel est obligatoire.")]
        [StringLength(50, ErrorMessage = "Le courriel ne peut pas dépasser {1} caractères.")]
        [EmailAddress(ErrorMessage = "Le format du courriel est invalide.")]
        [Display(Name = "Courriel")]
        public string Courriel { get; set; } = "";

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [Range(11111, 99999)]
        public int MotPasse { get; set; }

        // Clé étrangère vers TypesUtilisateur
        [Required(ErrorMessage = "Le type d'utilisateur est obligatoire.")]
        [StringLength(1)]
        [Column(TypeName = "nchar(1)")]
        [RegularExpression("^[ASU]$", ErrorMessage = "Le type doit être A, S ou U.")]
        [Display(Name = "Type d'utilisateur")]
        public string TypeUtilisateur { get; set; } = "U";

        // Propriété de navigation 
        [ForeignKey(nameof(TypeUtilisateur))]
        public virtual TypeUtilisateur? Type { get; set; }
    }
}