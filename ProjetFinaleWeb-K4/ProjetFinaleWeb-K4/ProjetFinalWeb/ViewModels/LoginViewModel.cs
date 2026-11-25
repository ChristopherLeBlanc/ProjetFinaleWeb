using System.ComponentModel.DataAnnotations;

namespace ProjetFinalWeb.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "L'identifiant est obligatoire.")]
        [Display(Name = "Identifiant")]
        public string Identifiant { get; set; } = "";

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; } = "";

    }
}