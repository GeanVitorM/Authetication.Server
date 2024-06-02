using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authetication.Server.Models
{
    public class Coordenador
    {
        [Key]
        public int IdCoordenador { get; set; }

        [MaxLength(100)]
        public string? NomeCoordenador { get; set; }

        [MaxLength(100)]
        public string? EmailCoordenador { get; set; }

        [MaxLength(100)]
        public string? PasswordCoordenador { get; set; }

        [ForeignKey("IdUser")]
        public Login? Login { get; set; }
    }
}
