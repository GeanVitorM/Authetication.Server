using System;
using System.Linq;

namespace Authetication.Server.Api.Middlewares
{
    public class RandomPassword
    {
        public string GerarSenhaAleatoria()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var senha = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return senha;
        }
    }
}
