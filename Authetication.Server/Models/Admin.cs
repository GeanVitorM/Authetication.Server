using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authetication.Server.Models
{
    public class Admin
    {
        [Key]
        public int IdAdmin { get; set; }

        public string? NomeAdmin { get; set; }
        public string? EmailAdmin { get; set; }
        public string? PasswordAdmin { get; set; }

        [ForeignKey("IdUser")]
        public Login? Login { get; set; }
    }
}
