using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Authetication.Server.Models;

public class Fisioterapeuta
{
    [Key]
    public int IdFisio { get; set; }

    [MaxLength(100)]
    public string? NomeFisio { get; set; }

    [MaxLength(100)]
    public string? EmailFisio { get; set; }

    public int Matricula { get; set; }

    [MaxLength(100)]
    public string? SemestreFisio { get; set; }

    public TipoUsuario TipoUsuario { get; set; }

    [ForeignKey("IdUser")]
    public Usuario? Usuario { get; set; }
}