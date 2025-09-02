using Microsoft.AspNetCore.Http;

namespace EstateManager.Application.DTOs;

public class AddImageDto
{
    public IFormFile File { get; set; } = default!;
}
