using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("Formats")]
    public class Format
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoFormat { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; } = "";

        public ICollection<Film> Films { get; set; } = new List<Film>();
    }
}
