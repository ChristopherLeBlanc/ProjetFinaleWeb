using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("Producteurs")]
    public class Producteur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoProducteur { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; } = "";

        public ICollection<Film> Films { get; set; } = new List<Film>();
    }
}
