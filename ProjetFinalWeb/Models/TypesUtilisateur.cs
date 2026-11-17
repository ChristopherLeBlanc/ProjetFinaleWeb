
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("TypesUtilisateur")]
    public class TypeUtilisateur
    {
        [Key]
        [Required]
        [StringLength(1)]
        [Column("TypeUtilisateur", TypeName = "nchar(1)")]
        [RegularExpression("^[ASU]$")]
        [Display(Name = "Type d'utilisateur")]
        public string Type { get; set; } = "";

        [Required]
        [StringLength(25)]
        [Display(Name = "Description")]
        public string Description { get; set; } = "";

        public virtual ICollection<Utilisateur>? Utilisateurs { get; set; }
    }
}
