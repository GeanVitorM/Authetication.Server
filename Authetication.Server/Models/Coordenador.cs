using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authetication.Server.Models
{
    public class Coordenador
    {
        [Key]
        public int IdCoordenador { get; set; }

        public string? NomeCoordenador { get; set; }
        public string? EmailCoordenador { get; set; }
        public string? PasswordCoordenador { get; set; }

        [ForeignKey("IdUser")]
        public Login? Login { get; set; }
    }
}
