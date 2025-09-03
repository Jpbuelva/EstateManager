using AutoMapper;
using EstateManager.Application.DTOs;
using EstateManager.Application.Services;
using EstateManager.Domain.Abstractions;
using EstateManager.Domain.Constants;
using EstateManager.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Moq;

namespace EstateManager.Tests.Services
{
    public class PropertyServiceTests
    {
        private readonly Mock<IPropertyRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PropertyService _service;

        public PropertyServiceTests()
        {
            _mockRepo = new Mock<IPropertyRepository>();
            _mockUow = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _service = new PropertyService(_mockRepo.Object, _mockUow.Object, _mockMapper.Object);
        }
        [Fact]
        public async Task CreateAsync_ShouldAddPropertyWithTrace_WhenInitialTraceExists()
        {
            var dto = new CreatePropertyDto
            {
                Name = "Casa nueva",
                Price = 1000000,
                InitialTrace = new PropertyTraceDto { Value = 1000000 }
            };

            var entity = new Property { Traces = new List<PropertyTrace>() };
            var trace = new PropertyTrace { Value = 1000000 };
            var expectedDto = new PropertyDto { IdProperty = 1, Name = "Casa nueva", Price = 1000000 };

            _mockMapper.Setup(m => m.Map<Property>(dto)).Returns(entity);
            _mockMapper.Setup(m => m.Map<PropertyTrace>(dto.InitialTrace)).Returns(trace);
            _mockMapper.Setup(m => m.Map<PropertyDto>(entity)).Returns(expectedDto);

            var result = await _service.CreateAsync(dto);

            Assert.Equal(expectedDto, result);
            Assert.Single(entity.Traces);
            Assert.Contains(entity.Traces, t => t.Name == PropertyTraceNames.Created);
            _mockRepo.Verify(r => r.AddAsync(entity), Times.Once);
            _mockUow.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdatePropertyAndAddTrace()
        {
            var dto = new UpdatePropertyDto { IdProperty = 1, Price = 900000 };
            var entity = new Property { IdProperty = 1, Price = 800000, Traces = new List<PropertyTrace>() };
            var expectedDto = new PropertyDto { IdProperty = 1, Name = "Actualizada", Price = 900000 };

            _mockRepo.Setup(r => r.GetByIdAsync(dto.IdProperty)).ReturnsAsync(entity);
            _mockMapper.Setup(m => m.Map(dto, entity));
            _mockMapper.Setup(m => m.Map<PropertyDto>(entity)).Returns(expectedDto);

            var result = await _service.UpdateAsync(dto);

            Assert.Equal(expectedDto, result);
            Assert.Contains(entity.Traces, t => t.Name == PropertyTraceNames.Updated);
            _mockRepo.Verify(r => r.UpdateAsync(entity), Times.Once);
            _mockUow.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnNull_WhenPropertyNotFound()
        {
            var dto = new UpdatePropertyDto { IdProperty = 99 };
            _mockRepo.Setup(r => r.GetByIdAsync(dto.IdProperty)).ReturnsAsync((Property)null);

            var result = await _service.UpdateAsync(dto);

            Assert.Null(result);
        }
        [Fact]
        public async Task ChangePriceAsync_ShouldUpdatePriceAndReturnDto()
        {
            var dto = new ChangePriceDto { IdProperty = 1, Price = 950000 };
            var entity = new Property { IdProperty = 1, Price = 800000 };
            var expectedDto = new PropertyDto { IdProperty = 1, Price = 950000 };

            _mockRepo.Setup(r => r.GetByIdAsync(dto.IdProperty)).ReturnsAsync(entity);
            _mockMapper.Setup(m => m.Map<PropertyDto>(entity)).Returns(expectedDto);

            var result = await _service.ChangePriceAsync(dto);

            Assert.Equal(expectedDto, result);
            Assert.Equal(dto.Price, entity.Price);
            _mockRepo.Verify(r => r.UpdateAsync(entity), Times.Once);
            _mockUow.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task ChangePriceAsync_ShouldReturnNull_WhenPropertyNotFound()
        {
            var dto = new ChangePriceDto { IdProperty = 99, Price = 950000 };
            _mockRepo.Setup(r => r.GetByIdAsync(dto.IdProperty)).ReturnsAsync((Property)null);

            var result = await _service.ChangePriceAsync(dto);

            Assert.Null(result);
        }
        [Fact]
        public async Task AddImageAsync_ShouldAddImageAndReturnDto()
        {
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.Length).Returns(1024);
            fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), default)).Returns(Task.CompletedTask);

            var dto = new AddImageDto { File = fileMock.Object };
            var entity = new Property { IdProperty = 1, Images = new List<PropertyImage>() };
            var expectedDto = new PropertyDto { IdProperty = 1 };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
            _mockMapper.Setup(m => m.Map<PropertyDto>(entity)).Returns(expectedDto);

            var result = await _service.AddImageAsync(1, dto);

            Assert.Equal(expectedDto, result);
            Assert.Single(entity.Images);
            _mockRepo.Verify(r => r.UpdateAsync(entity), Times.Once);
            _mockUow.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task AddImageAsync_ShouldThrow_WhenFileIsTooLarge()
        {
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.Length).Returns(6 * 1024 * 1024); // 6MB

            var dto = new AddImageDto { File = fileMock.Object };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.AddImageAsync(1, dto));
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedProperties()
        {
            var entities = new List<Property>
        {
            new Property { IdProperty = 1, Name = "Casa A" },
            new Property { IdProperty = 2, Name = "Casa B" }
        };

            var dtos = entities.Select(e => new PropertyDto { IdProperty = e.IdProperty, Name = e.Name }).ToList();

            _mockRepo.Setup(r => r.GetAllAsync(null, null, null)).ReturnsAsync(entities);
            foreach (var e in entities)
                _mockMapper.Setup(m => m.Map<PropertyDto>(e)).Returns(dtos.First(d => d.IdProperty == e.IdProperty));

            var result = await _service.GetAllAsync(null, null, null);

            Assert.Equal(dtos.Count, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnMappedDto_WhenFound()
        {
            var entity = new Property { IdProperty = 1, Name = "Casa A" };
            var dto = new PropertyDto { IdProperty = 1, Name = "Casa A" };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
            _mockMapper.Setup(m => m.Map<PropertyDto>(entity)).Returns(dto);

            var result = await _service.GetByIdAsync(1);

            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Property)null);

            var result = await _service.GetByIdAsync(99);

            Assert.Null(result);
        }
    }
}

