using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("Langues")]
    public class Langue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoLangue { get; set; }

        [Required]
        [StringLength(10)]
        [Column("Langue")]
        public string Nom { get; set; } = "";

        public virtual ICollection<FilmLangue> FilmsLangues { get; set; } = new List<FilmLangue>();
    }
}