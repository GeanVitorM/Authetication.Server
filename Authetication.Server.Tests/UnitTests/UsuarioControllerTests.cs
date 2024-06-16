using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authetication.Server.Api.Controllers;
using Authetication.Server.Api.Services;
using Authetication.Server.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Authetication.Server.Api.Tests
{
    public class UsuarioControllerTests
    {
        private readonly Mock<IUsuarioService> _serviceMock;
        private readonly Mock<ILogger<UsuarioController>> _loggerMock;
        private readonly UsuarioController _controller;

        public UsuarioControllerTests()
        {
            _serviceMock = new Mock<IUsuarioService>();
            _loggerMock = new Mock<ILogger<UsuarioController>>();
            _controller = new UsuarioController(_loggerMock.Object, _serviceMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WhenUsuariosExist()
        {
            // Arrange
            var usuarios = new List<UsuarioDto>
            {
                new UsuarioDto { IdUser = 1, Username = "user1" },
                new UsuarioDto { IdUser = 2, Username = "user2" }
            };
            _serviceMock.Setup(s => s.GetAllUsers()).ReturnsAsync(usuarios);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnUsuarios = Assert.IsAssignableFrom<IEnumerable<UsuarioDto>>(okResult.Value);
            Assert.Equal(2, returnUsuarios.Count());
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenUsuariosDoNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetAllUsers()).ReturnsAsync((IEnumerable<UsuarioDto>)null);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Get_ById_ReturnsOk_WhenUsuarioExists()
        {
            // Arrange
            var usuario = new UsuarioDto { IdUser = 1, Username = "user1" };
            _serviceMock.Setup(s => s.GetUsuarioById(1)).ReturnsAsync(usuario);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnUsuario = Assert.IsType<UsuarioDto>(okResult.Value);
            Assert.Equal(1, returnUsuario.IdUser);
        }

        [Fact]
        public async Task Get_ById_ReturnsNotFound_WhenUsuarioDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetUsuarioById(1)).ReturnsAsync((UsuarioDto)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtRoute_WhenUsuarioIsCreated()
        {
            // Arrange
            var usuario = new UsuarioDto { IdUser = 1, Username = "user1", TipoUsuario = Models.TipoUsuario.Admin };
            _serviceMock.Setup(s => s.CreateUsuario(usuario)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Post(usuario);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            var returnUsuario = Assert.IsType<UsuarioDto>(createdAtRouteResult.Value);
            Assert.Equal(1, returnUsuario.IdUser);
        }

        [Fact]
        public async Task Put_ReturnsOk_WhenUsuarioIsUpdated()
        {
            // Arrange
            var usuario = new UsuarioDto { IdUser = 1, Username = "user1" };
            _serviceMock.Setup(s => s.UpdateUsuario(usuario)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Put(usuario);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnUsuario = Assert.IsType<UsuarioDto>(okResult.Value);
            Assert.Equal(1, returnUsuario.IdUser);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenUsuarioIsDeleted()
        {
            // Arrange
            var usuario = new UsuarioDto { IdUser = 1, Username = "user1" };
            _serviceMock.Setup(s => s.GetUsuarioById(1)).ReturnsAsync(usuario);
            _serviceMock.Setup(s => s.DeleteUsuario(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnUsuario = Assert.IsType<UsuarioDto>(okResult.Value);
            Assert.Equal(1, returnUsuario.IdUser);
        }
    }
}
