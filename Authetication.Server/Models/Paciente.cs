using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authetication.Server.Models;

public class Paciente
{
    [Key]
    public int IdPaciente { get; set; }

    public string? NomePaciente { get; set; }
    public string? CPF { get; set; }
    public string? UF { get; set; }
    public string? Endereco { get; set; }
    public string? NumeroCasa { get; set; }
    public DateTime DataDeNascimento { get; set; }
    public char? Sexo { get; set; }
    public string? Proficao { get; set; }
    public string? DiagnosticoClinico { get; set; }
    public string? DiagnosticoFisio { get; set; }
    public string? EmailPaciente { get; set; }
    public TipoUsuario TipoUsuario { get; set; }

    public int? IdUser { get; set; }
    [ForeignKey("IdUser")]
    public Usuario? Usuario { get; set; }
}
