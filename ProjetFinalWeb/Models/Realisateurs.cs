using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("Realisateurs")]
    public class Realisateur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoRealisateur { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; } = "";

        public ICollection<Film> Films { get; set; } = new List<Film>();
    }
}
