using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetFinalWeb.Models
{
    [Table("Categories")]
    public class Categorie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoCategorie { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; } = "";


    }
}
