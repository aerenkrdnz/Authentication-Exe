using System.ComponentModel.DataAnnotations;

namespace AuthenticationExe.WebUI.Models
{
    public class RegisterFormViewModel
    {
        [Display(Name = "Adı")]
        [MaxLength(25, ErrorMessage = "İsim maksimum 25 karakter uzunluğunda olabilir.")]
        [Required(ErrorMessage = "Ad alanı boş bırakılamaz.")]
        public string FirstName { get; set; }
        [Display(Name = "Soyadı")]
        [MaxLength(25, ErrorMessage = "Soyad en fazla 25 karakter uzunluğunda olabilir.")]
        [Required(ErrorMessage = "Soyad alanı boş bırakılamaz.")]
        public string LastName { get; set; }
        [Display(Name = "Eposta Adresi")]
        [Required(ErrorMessage = "Eposta alanı boş bırakılamaz.")]
        public string Email { get; set; }
        [Display(Name = "Şifre")]
        [MinLength(5, ErrorMessage = "Bu şifreyi tahmin etmek çok kolay. Lütfen yeni bir şifre oluştur. ")]
        [MaxLength(55, ErrorMessage = "Soyad en fazla 55 karakter uzunluğunda olabilir.")]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        public string Password { get; set; }
        [Display(Name = "Şifre tekrarı")]
        [MinLength(5, ErrorMessage = "Bu şifreyi tahmin etmek çok kolay. Lütfen yeni bir şifre oluştur. ")]
        [MaxLength(55, ErrorMessage = "Soyad en fazla 55 karakter uzunluğunda olabilir.")]
        [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz.")]
        [Compare(nameof(Password), ErrorMessage = "Şifreler eşleşmiyor.")]
        public string PasswordConfirm { get; set; }        
        
    }
}
