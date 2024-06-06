using Authetication.Server.DTOs;
using Authetication.Server.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authetication.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;
        private readonly IAdminService _adminService;
        private readonly IPacienteService _pacienteService;
        private readonly IFisioterapeutaService _fisioService;
        private readonly ICoordenadorService _coordService;

        public AuthService(IConfiguration configuration, IUsuarioService usuarioService, IAdminService adminService, IPacienteService pacienteService, IFisioterapeutaService fisioService, ICoordenadorService coordService)
        {
            _configuration = configuration;
            _usuarioService = usuarioService;
            _adminService = adminService;
            _pacienteService = pacienteService;
            _fisioService = fisioService;
            _coordService = coordService;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            var usuarioDto = await _usuarioService.GetUsuarioByUsernameAndPassword(username, password);

            if (usuarioDto == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuarioDto.Login),
                new Claim("UserId", usuarioDto.IdUser.ToString()),
            };
            switch (usuarioDto.TipoUsuario)
            {
                case TipoUsuario.Admin:
                    var admin = await _adminService.GetAdminById(usuarioDto.IdUser);
                    claims.Add(new Claim("Role", TipoUsuario.Admin.ToString()));
                    claims.Add(new Claim("Nome", admin.NomeAdmin));
                    claims.Add(new Claim("Email", admin.EmailAdmin));
                    break;
                case TipoUsuario.Coordenador:
                    var coordenador = await _coordService.GetCoordById(usuarioDto.IdUser);
                    claims.Add(new Claim("Role", TipoUsuario.Coordenador.ToString()));
                    claims.Add(new Claim("Nome", coordenador.NomeCoordenador));
                    claims.Add(new Claim("Email", coordenador.EmailCoordenador));
                    break;
                case TipoUsuario.Fisioterapeuta:
                    var fisioterapeuta = await _fisioService.GetFisioById(usuarioDto.IdUser);
                    claims.Add(new Claim("Role", TipoUsuario.Fisioterapeuta.ToString()));
                    claims.Add(new Claim("Nome", fisioterapeuta.NomeFisio));
                    claims.Add(new Claim("Email", fisioterapeuta.EmailFisio));
                    claims.Add(new Claim("Matricula", fisioterapeuta.Matricula.ToString())); 
                    claims.Add(new Claim("Semestre", fisioterapeuta.SemestreFisio));
                    break;
                case TipoUsuario.Paciente:
                    var paciente = await _pacienteService.GetPacienteById(usuarioDto.IdUser);
                    claims.Add(new Claim("Role", TipoUsuario.Paciente.ToString()));
                    claims.Add(new Claim("Nome", paciente.NomePaciente));
                    claims.Add(new Claim("Email", paciente.EmailPaciente));
                    claims.Add(new Claim("CPF", paciente.CPF));
                    claims.Add(new Claim("UF", paciente.UF));
                    claims.Add(new Claim("Endereço", paciente.Endereco));
                    claims.Add(new Claim("Numero Casa", paciente.NumeroCasa.ToString())); 
                    claims.Add(new Claim("Data Nascimento", paciente.DataDeNascimento.ToString("yyyy-MM-dd"))); 
                    claims.Add(new Claim("Sexo", paciente.Sexo.ToString()));
                    claims.Add(new Claim("Proficao", paciente.Proficao));
                    claims.Add(new Claim("Diagnostico fisio", paciente.DiagnosticoFisio));
                    claims.Add(new Claim("Diagnostico clinico", paciente.DiagnosticoClinico));
                    break;
                default:
                    break;
            }





            // Geração do token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
