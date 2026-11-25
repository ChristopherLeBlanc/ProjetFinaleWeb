using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("FilmsActeurs")]
    public class FilmActeur
    {
        public int NoFilm { get; set; }
        public int NoActeur { get; set; }

        [ForeignKey(nameof(NoFilm))]
        public virtual Film? Film { get; set; }

        [ForeignKey(nameof(NoActeur))]
        public virtual Acteur? Acteur { get; set; }
    }
}