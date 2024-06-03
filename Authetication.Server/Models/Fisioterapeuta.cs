using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authetication.Server.Models;

public class Fisioterapeuta
{
    [Key]
    public int IdFisio { get; set; }

    [MaxLength(100)]
    public string? NomeFisio { get; set; }

    [MaxLength(100)]
    public string? EmailFisio { get; set; }

    [MaxLength(100)]
    public int Matricula { get; set; }

    [MaxLength(100)]
    public string? SemestreFisio { get; set; }

    [MaxLength(100)]
    public string? PasswordFisio { get; set; }

    [ForeignKey("IdUser")]
    public Usuario? Usuario { get; set; }
}
