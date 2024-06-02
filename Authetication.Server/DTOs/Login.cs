using System.ComponentModel.DataAnnotations;

namespace Authetication.Server.DTOs;

public class Login
{

    [Key]
    public int IdUser { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Type is required")]
    public string? TipoUsuario { get; set; }

    public Admin? Admin { get; set; }
    public Paciente? Paciente { get; set; }
    public Fisioterapeuta? Fisioterapeuta { get; set; }
    public Coordenador? Coordenador { get; set; }
}
