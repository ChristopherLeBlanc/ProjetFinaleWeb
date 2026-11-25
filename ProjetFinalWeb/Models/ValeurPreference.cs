
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("ValeursPreferences")]
    public class ValeurPreference
    {
        [Column("NoUtilisateur")]
        public int NoUtilisateur { get; set; }

        [Column("NoPreference")]
        public int NoPreference { get; set; }

        [Required(ErrorMessage = "La valeur est obligatoire.")]
        [StringLength(50, ErrorMessage = "La valeur ne peut pas dépasser 50 caractères.")]
        [Display(Name = "Valeur")]
        public string Valeur { get; set; } = "";

        // Relations
        [ForeignKey(nameof(NoUtilisateur))]
        public virtual Utilisateur? Utilisateur { get; set; }

        [ForeignKey(nameof(NoPreference))]
        public virtual Preference? Preference { get; set; }
    }
}
