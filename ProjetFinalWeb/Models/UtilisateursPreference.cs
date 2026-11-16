
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("UtilisateursPreferences")]
    public class UtilisateursPreference
    {
        [Column("NoUtilisateur")]
        public int NoUtilisateur { get; set; }

        [Column("NoPreference")]
        public int NoPreference { get; set; }

        // Relations
        [ForeignKey(nameof(NoUtilisateur))]
        public virtual Utilisateur? Utilisateur { get; set; }

        [ForeignKey(nameof(NoPreference))]
        public virtual Preference? Preference { get; set; }
    }
}
