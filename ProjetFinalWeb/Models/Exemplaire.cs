
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("Exemplaires")]
    public class Exemplaire
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoExemplaire { get; set; }

        public int NoUtilisateurProprietaire { get; set; }

        [ForeignKey(nameof(NoUtilisateurProprietaire))]
        public virtual Utilisateur? Proprietaire { get; set; }

        public virtual ICollection<EmpruntFilm> Emprunts { get; set; }
            = new List<EmpruntFilm>();
    }
}
