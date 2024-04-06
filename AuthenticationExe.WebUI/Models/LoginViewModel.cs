using System.ComponentModel.DataAnnotations;

namespace AuthenticationExe.WebUI.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email Adresi")]
        [Required(ErrorMessage = "Eposta alanı boş bırakılamaz.")]
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        public string Password { get; set; }
    }
}
