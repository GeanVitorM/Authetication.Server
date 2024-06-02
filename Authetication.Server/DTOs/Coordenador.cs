using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Authetication.Server.DTOs;

public class Coordenador
{
    [Key]
    public int IdCoordenador { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string? NomeCoordenador { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
    public string? EmailCoordenador { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? PasswordCoordenador { get; set; }

    [ForeignKey("IdUser")]
    public Login? Login { get; set; }
}
