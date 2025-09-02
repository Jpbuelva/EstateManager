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
            Name = dto.Name,
            Price = dto.Price,
            Year = dto.Year,
            CodeInternal = dto.CodeInternal,
            IdOwner = dto.IdOwner
        };

        await _propertyRepository.AddAsync(property);
        await _unitOfWork.CommitAsync();

        return MapToDto(property);
    }

    public async Task<PropertyDto?> UpdateAsync(UpdatePropertyDto dto)
    {
        var property = await _propertyRepository.GetByIdAsync(dto.IdProperty);
        if (property == null) return null;

        property.Address = dto.Address;
        property.Name = dto.Name;
        property.Price = dto.Price;
        property.Year = dto.Year;

        await _propertyRepository.UpdateAsync(property);
        await _unitOfWork.CommitAsync();

        return MapToDto(property);
    }

    public async Task<PropertyDto?> ChangePriceAsync(ChangePriceDto dto)
    {
        var property = await _propertyRepository.GetByIdAsync(dto.IdProperty);
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

        using var ms = new MemoryStream();
        await dto.File.CopyToAsync(ms);

        var base64String = Convert.ToBase64String(ms.ToArray());

        var image = new PropertyImage
        {
            IdProperty = propertyId,
            File = base64String,
            Enabled = true
        };

        property.Images.Add(image);

        await _propertyRepository.UpdateAsync(property);
        await _unitOfWork.CommitAsync();

        return MapToDto(property);
    }

    public async Task<IEnumerable<PropertyDto>> GetAllAsync(string? name, decimal? minPrice, decimal? maxPrice)
    {
        var properties = await _propertyRepository.GetAllAsync(name, minPrice, maxPrice);
        return properties.Select(MapToDto);
    }

    public async Task<PropertyDto?> GetByIdAsync(int idProperty)
    {
        var property = await _propertyRepository.GetByIdAsync(idProperty);
        return property == null ? null : MapToDto(property);
    }

    private PropertyDto MapToDto(Property property) =>
      new PropertyDto
      {
          IdProperty = property.IdProperty,
          Name = property.Name,
          Address = property.Address,
          Price = property.Price,
          Year = property.Year,
          IdOwner = property.IdOwner
      };

}
