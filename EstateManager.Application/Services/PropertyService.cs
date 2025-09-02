using AutoMapper;
using EstateManager.Application.DTOs;
using EstateManager.Application.Interfaces;
using EstateManager.Domain.Abstractions;
using EstateManager.Domain.Constants;
using EstateManager.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace EstateManager.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;  

    private const int MaxFileSizeBytes = 5 * 1024 * 1024; 

    public PropertyService(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _propertyRepository = propertyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PropertyDto> CreateAsync(CreatePropertyDto dto)
    {
    
        var property = _mapper.Map<Property>(dto);

        
        if (dto.InitialTrace != null)
        {
            var trace = _mapper.Map<PropertyTrace>(dto.InitialTrace);
            trace.Name= PropertyTraceNames.Created;
            property.Traces.Add(trace);
        }

        await _propertyRepository.AddAsync(property).ConfigureAwait(false);
        await _unitOfWork.CommitAsync().ConfigureAwait(false);

        return _mapper.Map<PropertyDto>(property);
    }



    public async Task<PropertyDto?> UpdateAsync(UpdatePropertyDto dto)
    {
        var property = await GetPropertyByIdAsync(dto.IdProperty);
        if (property == null) return null;
 
        var trace = new PropertyTrace
        {
            DateSale = DateTime.UtcNow,
            Name = PropertyTraceNames.Updated,
            Value = property.Price,
            Tax = 0  
        };
        property.Traces.Add(trace);

         
        _mapper.Map(dto, property);

        await _propertyRepository.UpdateAsync(property);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<PropertyDto>(property);
    }


    public async Task<PropertyDto?> ChangePriceAsync(ChangePriceDto dto)
    {
        var property = await GetPropertyByIdAsync(dto.IdProperty);
        if (property == null) return null;

        property.Price = dto.Price;

        await _propertyRepository.UpdateAsync(property).ConfigureAwait(false);
        await _unitOfWork.CommitAsync().ConfigureAwait(false);

        return _mapper.Map<PropertyDto>(property);
    }

    public async Task<PropertyDto?> AddImageAsync(int propertyId, AddImageDto dto)
    {
        ValidateImage(dto);

        var property = await GetPropertyByIdAsync(propertyId);
        if (property == null) return null;

        var image = new PropertyImage
        {
            IdProperty = propertyId,
            File = await ConvertToBase64Async(dto.File),
            Enabled = true
        };

        property.Images.Add(image);

        await _propertyRepository.UpdateAsync(property).ConfigureAwait(false);
        await _unitOfWork.CommitAsync().ConfigureAwait(false);

        return _mapper.Map<PropertyDto>(property);
    }

    public async Task<IEnumerable<PropertyDto>> GetAllAsync(string? name, decimal? minPrice, decimal? maxPrice)
    {
        var properties = await _propertyRepository.GetAllAsync(name, minPrice, maxPrice)
                                                  .ConfigureAwait(false);
        return properties.Select(p => _mapper.Map<PropertyDto>(p));
    }

    public async Task<PropertyDto?> GetByIdAsync(int idProperty)
    {
        var property = await _propertyRepository.GetByIdAsync(idProperty);
        return property == null ? null : _mapper.Map<PropertyDto>(property);
    }

    #region Private helpers

    private async Task<Property?> GetPropertyByIdAsync(int idProperty) =>
        await _propertyRepository.GetByIdAsync(idProperty).ConfigureAwait(false);
 

    private void ValidateImage(AddImageDto dto)
    {
        if (dto.File == null || dto.File.Length == 0)
            throw new ArgumentException("File cannot be empty.");
        if (dto.File.Length > MaxFileSizeBytes)
            throw new ArgumentException("File exceeds maximum allowed size (5MB).");
    }

    private async Task<string> ConvertToBase64Async(IFormFile file)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms).ConfigureAwait(false);
        return Convert.ToBase64String(ms.ToArray());
    }

    #endregion
}
