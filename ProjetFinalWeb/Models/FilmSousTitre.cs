using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("FilmsSousTitres")]
    public class FilmSousTitre
    {
        public int NoFilm { get; set; }
        public int NoSousTitre { get; set; }

        [ForeignKey(nameof(NoFilm))]
        public virtual Film? Film { get; set; }

        [ForeignKey(nameof(NoSousTitre))]
        public virtual SousTitre? SousTitre { get; set; }
    }
}