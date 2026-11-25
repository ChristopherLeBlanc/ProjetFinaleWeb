using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("FilmsSupplements")]
    public class FilmSupplement
    {
        public int NoFilm { get; set; }
        public int NoSupplement { get; set; }

        [ForeignKey(nameof(NoFilm))]
        public virtual Film? Film { get; set; }

        [ForeignKey(nameof(NoSupplement))]
        public virtual Supplement? Supplement { get; set; }
    }
}