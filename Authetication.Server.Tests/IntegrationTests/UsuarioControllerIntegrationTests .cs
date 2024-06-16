using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Authetication.Server.Api.DTOs;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace Authetication.Server.Api.Tests
{
    public class UsuarioControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UsuarioControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");
            }).CreateClient();
        }

        [Fact]
        public async Task Get_Usuarios_ReturnsOkResponse()
        {
            var response = await _client.GetAsync("/usuario");
            response.EnsureSuccessStatusCode();
            var usuarios = await response.Content.ReadFromJsonAsync<IEnumerable<UsuarioDto>>();
            Assert.NotNull(usuarios);
        }

        [Fact]
        public async Task Get_UsuarioById_ReturnsOkResponse()
        {
            var id = 1; // Substitua por um ID válido
            var response = await _client.GetAsync($"/usuario/{id}");
            response.EnsureSuccessStatusCode();
            var usuario = await response.Content.ReadFromJsonAsync<UsuarioDto>();
            Assert.NotNull(usuario);
            Assert.Equal(id, usuario.IdUser);
        }

        [Fact]
        public async Task Post_CreatesNewUsuario_ReturnsCreatedAtRoute()
        {
            var newUsuario = new UsuarioDto
            {
                Username = "newuser@example.com",
                Password = "TesteUnit",
                TipoUsuario = Models.TipoUsuario.Paciente
            };
            var response = await _client.PostAsJsonAsync("/usuario", newUsuario);
            response.EnsureSuccessStatusCode();
            var createdUsuario = await response.Content.ReadFromJsonAsync<UsuarioDto>();
            Assert.NotNull(createdUsuario);
            Assert.Equal(newUsuario.Username, createdUsuario.Username);
        }

        [Fact]
        public async Task Put_UpdateUsuario_ReturnsOkResponse()
        {
            var updatedUsuario = new UsuarioDto
            {
                IdUser = 1, // Substitua por um ID válido
                Username = "updateduser@example.com",
                TipoUsuario = Models.TipoUsuario.Admin
            };
            var response = await _client.PutAsJsonAsync("/usuario", updatedUsuario);
            response.EnsureSuccessStatusCode();
            var retrievedUsuario = await response.Content.ReadFromJsonAsync<UsuarioDto>();
            Assert.Equal(updatedUsuario.Username, retrievedUsuario.Username);
        }

        [Fact]
        public async Task Delete_RemoveUsuario_ReturnsOkResponse()
        {
            var id = 1; // Substitua por um ID válido
            var response = await _client.DeleteAsync($"/usuario/{id}");
            response.EnsureSuccessStatusCode();
            var getResponse = await _client.GetAsync($"/usuario/{id}");
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}
