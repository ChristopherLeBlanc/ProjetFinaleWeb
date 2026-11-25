
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("Preferences")]
    public class Preference
    {
        [Key]
        [Column("NoPreference")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoPreference { get; set; }

        [Required(ErrorMessage = "La description est obligatoire.")]
        [StringLength(50, ErrorMessage = "La description ne peut pas dépasser 50 caractères.")]
        [Display(Name = "Description")]
        public string Description { get; set; } = "";
    }
}
