using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Authetication.Server.DTOs;

public class Paciente
{
    [Key]
    public int IdPaciente { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string? NomePaciente { get; set; }

    [Required(ErrorMessage = "Cpf is required")]
    public string? CPF { get; set; }

    [Required(ErrorMessage = "Uf is required")]
    public string? UF { get; set; }

    [Required(ErrorMessage = "Endereco is required")]
    public string? Endereco { get; set; }

    [Required(ErrorMessage = "NumeroCasa is required")]
    public string? NumeroCasa { get; set; }

    [Required(ErrorMessage = "Data is required")]
    public DateTime DataDeNascimento { get; set; }

    [Required(ErrorMessage = "Sexo is required")]
    public char[] Sexo { get; set; }

    [Required(ErrorMessage = "Proficao is required")]
    public string? Proficao { get; set; }

    [Required(ErrorMessage = "Diagnostico is required")]
    public string? DiagnosticoClinico { get; set; }

    [Required(ErrorMessage = "Diagnostico is required")]
    public string? DiagnosticoFisio { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
    public string? EmailPaciente { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? PasswordPaciente { get; set; }

    [ForeignKey("IdUser")]
    public Login? Login { get; set; }
}
