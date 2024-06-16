using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Authetication.Server.Api.Models;

namespace Authetication.Server.Api.DTOs;

public class CoordenadorDto
{
    [Key]
    public int IdCoordenador { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string? NomeCoordenador { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
    public string? EmailCoordenador { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public TipoUsuario TipoUsuario { get; set; }

    [ForeignKey("IdUser")]
    public UsuarioDto? Usuario { get; set; }
}