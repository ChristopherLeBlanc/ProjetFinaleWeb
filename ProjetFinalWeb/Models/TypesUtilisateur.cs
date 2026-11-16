using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("TypesUtilisateur")]
    public class TypeUtilisateur
    {
        // Propriété de clé
        [Key]
        [Column("TypeUtilisateur", TypeName = "nchar(1)")]
        [Required(ErrorMessage = "Le type d'utilisateur est obligatoire.")]
        [StringLength(1, ErrorMessage = "Le type doit contenir un seul caractère.")]
        [RegularExpression("^[ASU]$", ErrorMessage = "Le type doit être A, S ou U.")]
        [Display(Name = "Type d'utilisateur")]
        public string Type { get; set; } = "";

        [Required(ErrorMessage = "La description est obligatoire.")]
        [StringLength(25, ErrorMessage = "La description ne peut pas dépasser {1} caractères.")]
        [Display(Name = "Description")]
        public string Description { get; set; } = "";

        // Propriété de navigation (un type -> plusieurs utilisateurs)
        public virtual ICollection<Utilisateur>? Utilisateurs { get; set; }
    }
}