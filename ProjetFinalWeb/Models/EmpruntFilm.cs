
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("EmpruntsFilms")]
    public class EmpruntFilm
    {
        public int NoExemplaire { get; set; }

        public int NoUtilisateur { get; set; }

        public DateTime DateEmprunt { get; set; }

        [ForeignKey(nameof(NoExemplaire))]
        public virtual Exemplaire? Exemplaire { get; set; }

        [ForeignKey(nameof(NoUtilisateur))]
        public virtual Utilisateur? Utilisateur { get; set; }
    }
}
