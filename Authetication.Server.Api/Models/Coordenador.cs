using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authetication.Server.Api.Models;

public class Coordenador
{
    [Key]
    public int IdCoordenador { get; set; }

    [MaxLength(100)]
    public string? NomeCoordenador { get; set; }

    [MaxLength(100)]
    public string? EmailCoordenador { get; set; }

    public TipoUsuario TipoUsuario { get; set; }

    [ForeignKey("IdUser")]
    public Usuario? Usuario { get; set; }
}
