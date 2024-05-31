using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authetication.Server.Models
{
    public class Login
    {
        [Key]
        public int IdUser { get; set; }

        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? TipoUsuario { get; set; }

        // Navegação para as outras entidades
        public Admin? Admin { get; set; }
        public Paciente? Paciente { get; set; }
        public Fisioterapeuta? Fisioterapeuta { get; set; }
        public Coordenador? Coordenador { get; set; }
    }
}
