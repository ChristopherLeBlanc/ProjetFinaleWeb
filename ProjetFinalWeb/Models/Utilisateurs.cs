
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProjetFinalWeb.Models
{
    [Table("Utilisateurs")]
    public class Utilisateur : IdentityUser<int>
    {
        [NotMapped]
        public int NoUtilisateur
        {
            get { return Id; }
            set { Id = value; }
        }

        [NotMapped]
        [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire.")]
        [StringLength(10, MinimumLength = 2,
            ErrorMessage = "Le nom d'utilisateur doit contenir entre 2 et 10 caractères.")]
        [RegularExpression(@"^[A-Za-z][A-Za-z-]*[A-Za-z]$",
            ErrorMessage = "Le nom doit débuter et se terminer par une lettre.")]
        [Display(Name = "Nom d'utilisateur")]
        public string NomUtilisateur
        {
            get { return UserName ?? ""; }
            set { UserName = value; }
        }

        [NotMapped]
        [Required(ErrorMessage = "Le courriel est obligatoire.")]
        [StringLength(25)]
        [EmailAddress(ErrorMessage = "Le format du courriel est invalide.")]
        [Display(Name = "Courriel")]
        public string Courriel
        {
            get { return Email ?? ""; }
            set { Email = value; }
        }

        [Required(ErrorMessage = "Le type d'utilisateur est obligatoire.")]
        [StringLength(1)]
        [Column(TypeName = "nchar(1)")]
        [RegularExpression("^[ASU]$")]
        [Display(Name = "Type d'utilisateur")]
        public string TypeUtilisateur { get; set; } = "U";

        [ForeignKey(nameof(TypeUtilisateur))]
        public virtual TypeUtilisateur? Type { get; set; }
    }
}
