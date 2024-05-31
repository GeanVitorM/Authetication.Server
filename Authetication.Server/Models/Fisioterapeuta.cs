using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authetication.Server.Models
{
    public class Fisioterapeuta
    {
        [Key]
        public int IdFisio { get; set; }

        public string? NomeFisio { get; set; }
        public string? EmailFisio { get; set; }
        public int Matricula { get; set; }
        public string? SemestreFisio { get; set; }
        public string? PasswordFisio { get; set; }

        [ForeignKey("IdUser")]
        public Login? Login { get; set; }
    }
}
