using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Authetication.Server.Api.Controllers;
using Authetication.Server.Api.Services;
using Authetication.Server.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authentication.Server.Tests.UnitTests
{
    public class AdminControllerTests
    {
        private readonly Mock<ILogger<AdminController>> _mockLogger;
        private readonly Mock<IAdminService> _mockService;
        private readonly AdminController _controller;

        public AdminControllerTests()
        {
            _mockLogger = new Mock<ILogger<AdminController>>();
            _mockService = new Mock<IAdminService>();
            _controller = new AdminController(_mockLogger.Object, _mockService.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithListOfAdmins()
        {
            // Arrange
            _mockService.Setup(service => service.GetAllAdmins())
                        .ReturnsAsync(new List<AdminDto> { new AdminDto() });

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<AdminDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenNoAdminsExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetAllAdmins())
                        .ReturnsAsync((IEnumerable<AdminDto>)null);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        // More tests for other methods
    }
}
