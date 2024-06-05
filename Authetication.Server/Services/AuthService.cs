using Authetication.Server.DTOs;
using Authetication.Server.Models;
//using Authetication.Server.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
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

        public AuthService(IConfiguration configuration, IUsuarioService usuarioService)
        {
            _configuration = configuration;
            _usuarioService = usuarioService;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            // Validação do usuário
            var usuarioDto = await _usuarioService.GetUsuarioByUsernameAndPassword(username, password);

            if (usuarioDto == null)
            {
                return null;
            }

            // Mapeamento das informações do usuário para claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, usuarioDto.Username),
        // Adicione outras reivindicações conforme necessário
    };

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
