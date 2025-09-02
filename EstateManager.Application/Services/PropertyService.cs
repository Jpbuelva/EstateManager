using EstateManager.Application.DTOs;
using EstateManager.Application.Interfaces;
using EstateManager.Domain.Abstractions;
using EstateManager.Domain.Entities;

namespace EstateManager.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PropertyService(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork)
    {
        _propertyRepository = propertyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PropertyDto> CreateAsync(CreatePropertyDto dto)
    {
        var property = new Property
        {
            Address = dto.Address,
            City = dto.City,
            State = dto.State,
            Price = dto.Price
        };

        await _propertyRepository.AddAsync(property);
        await _unitOfWork.CommitAsync();

        return MapToDto(property);
    }

    public async Task<PropertyDto?> UpdateAsync(int id, UpdatePropertyDto dto)
    {
        var property = await _propertyRepository.GetByIdAsync(id);
        if (property == null) return null;

        property.Address = dto.Address;
        property.City = dto.City;
        property.State = dto.State;
        property.Price = dto.Price;

        await _propertyRepository.UpdateAsync(property);
        await _unitOfWork.CommitAsync();

        return MapToDto(property);
    }

    public async Task<PropertyDto?> ChangePriceAsync(int id, ChangePriceDto dto)
    {
        var property = await _propertyRepository.GetByIdAsync(id);
        if (property == null) return null;

        property.Price = dto.Price;

        await _propertyRepository.UpdateAsync(property);
        await _unitOfWork.CommitAsync();

        return MapToDto(property);
    }

    public async Task<PropertyDto?> AddImageAsync(int propertyId, AddImageDto dto)
    {
        var property = await _propertyRepository.GetByIdAsync(propertyId);
        if (property == null) return null;

        var image = new PropertyImage { Url = dto.Url, PropertyId = propertyId };
        property.Images.Add(image);

        await _propertyRepository.UpdateAsync(property);
        await _unitOfWork.CommitAsync();

        return MapToDto(property);
    }

    public async Task<IEnumerable<PropertyDto>> GetAllAsync(string? city, decimal? minPrice, decimal? maxPrice)
    {
        var properties = await _propertyRepository.GetAllAsync(city, minPrice, maxPrice);
        return properties.Select(MapToDto);
    }

    public async Task<PropertyDto?> GetByIdAsync(int id)
    {
        var property = await _propertyRepository.GetByIdAsync(id);
        return property == null ? null : MapToDto(property);
    }

    private PropertyDto MapToDto(Property property) =>
        new PropertyDto
        {
            Id = property.Id,
            Address = property.Address,
            City = property.City,
            State = property.State,
            Price = property.Price,
            CreatedAt = property.CreatedAt
        };
}
