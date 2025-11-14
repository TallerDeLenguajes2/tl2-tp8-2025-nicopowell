using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels;

public class LoginViewModel
{
    [Display(Name = "Username")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "El username debe tener entre 4 y 50 caracteres.")]
    [Required(ErrorMessage = "El username es obligatorio.")]
    public string Username { get; set; }

    [Display(Name = "Password")]
    [StringLength(75, MinimumLength = 4, ErrorMessage = "El password debe tener entre 4 y 75 caracteres.")]
    [Required(ErrorMessage = "El password es obligatorio.")]
    public string Password { get; set; }

    public string ErrorMessage { get; set; }
}
