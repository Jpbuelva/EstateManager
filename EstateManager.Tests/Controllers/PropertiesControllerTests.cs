using EstateManager.API.Controllers;
using EstateManager.Application.DTOs;
using EstateManager.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace EstateManager.Tests.Controllers
{
    public class PropertiesControllerTests
    {
        private readonly Mock<IPropertyService> _mockService;
        private readonly PropertiesController _controller;

        public PropertiesControllerTests()
        {
            _mockService = new Mock<IPropertyService>();
            _controller = new PropertiesController(_mockService.Object);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WhenSuccessful()
        {
            var dto = new CreatePropertyDto { Name = "Casa nueva", Price = 1000000 };
            var created = new PropertyDto { IdProperty = 1, Name = dto.Name, Price = dto.Price };

            _mockService.Setup(s => s.CreateAsync(dto)).ReturnsAsync(created);

            var result = await _controller.Create(dto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetById), createdResult.ActionName);
            Assert.Equal(created, createdResult.Value);
        }

        [Fact]
        public async Task GetAll_ReturnsFilteredProperties()
        {
            var expected = new List<PropertyDto>
        {
            new PropertyDto { IdProperty = 1, Name = "Casa A", Price = 500000 },
            new PropertyDto { IdProperty = 2, Name = "Casa B", Price = 800000 }
        };

            _mockService.Setup(s => s.GetAllAsync("Casa", 400000, 900000)).ReturnsAsync(expected);

            var result = await _controller.GetAll("Casa", 400000, 900000);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, okResult.Value);
        }
        [Fact]
        public async Task GetById_ReturnsProperty_WhenFound()
        {
            var property = new PropertyDto { IdProperty = 1, Name = "Casa A", Price = 500000 };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(property);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(property, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMissing()
        {
            _mockService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((PropertyDto)null);

            var result = await _controller.GetById(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsUpdatedProperty_WhenSuccessful()
        {
            var dto = new UpdatePropertyDto { IdProperty = 1, Price = 600000 };
            var updated = new PropertyDto { IdProperty = 1, Price = dto.Price };

            _mockService.Setup(s => s.UpdateAsync(dto)).ReturnsAsync(updated);

            var result = await _controller.Update(dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(updated, okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenPropertyMissing()
        {
            var dto = new UpdatePropertyDto { IdProperty = 99, Price = 600000 };
            _mockService.Setup(s => s.UpdateAsync(dto)).ReturnsAsync((PropertyDto)null);

            var result = await _controller.Update(dto);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ChangePrice_ReturnsUpdatedProperty_WhenSuccessful()
        {
            var dto = new ChangePriceDto { IdProperty = 1, Price = 750000 };
            var updated = new PropertyDto { IdProperty = 1, Name = "Casa A", Price = dto.Price };

            _mockService.Setup(s => s.ChangePriceAsync(dto)).ReturnsAsync(updated);

            var result = await _controller.ChangePrice(dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(updated, okResult.Value);
        }

        [Fact]
        public async Task ChangePrice_ReturnsNotFound_WhenPropertyMissing()
        {
            var dto = new ChangePriceDto { IdProperty = 99, Price = 750000 };
            _mockService.Setup(s => s.ChangePriceAsync(dto)).ReturnsAsync((PropertyDto)null);

            var result = await _controller.ChangePrice(dto);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task AddImage_ReturnsUpdatedProperty_WhenSuccessful()
        {
            var dto = new AddImageDto { File = new FormFile(null, 0, 0, "image", "image.jpg") };
            var updated = new PropertyDto { IdProperty = 1, Name = "Casa A", Price = 500000 };

            _mockService.Setup(s => s.AddImageAsync(1, dto)).ReturnsAsync(updated);

            var result = await _controller.AddImage(1, dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(updated, okResult.Value);
        }

        [Fact]
        public async Task AddImage_ReturnsNotFound_WhenPropertyMissing()
        {
            var dto = new AddImageDto { File = new FormFile(null, 0, 0, "image", "image.jpg") };
            _mockService.Setup(s => s.AddImageAsync(99, dto)).ReturnsAsync((PropertyDto)null);

            var result = await _controller.AddImage(99, dto);

            Assert.IsType<NotFoundResult>(result);
        }


    }
}
