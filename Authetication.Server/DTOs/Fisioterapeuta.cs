using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Authetication.Server.DTOs;

public class Fisioterapeuta
{
    [Key]
    public int IdFisio { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string? NomeFisio { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
    public string? EmailFisio { get; set; }

    [Required(ErrorMessage = "Matricula is required")]
    public int Matricula { get; set; }

    [Required(ErrorMessage = "Semestre is required")]
    public string? SemestreFisio { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? PasswordFisio { get; set; }

    [ForeignKey("IdUser")]
    public Login? Login { get; set; }
}
