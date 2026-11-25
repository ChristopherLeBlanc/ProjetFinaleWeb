using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("FilmsLangues")]
    public class FilmLangue
    {
        public int NoFilm { get; set; }
        public int NoLangue { get; set; }

        [ForeignKey(nameof(NoFilm))]
        public virtual Film? Film { get; set; }

        [ForeignKey(nameof(NoLangue))]
        public virtual Langue? Langue { get; set; }
    }
}