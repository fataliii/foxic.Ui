using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Foxic.Buisness.ViewModels.AreasViewModels.BrandVM;

public class BrandPostVM
{
    [Required, MaxLength(30), MinLength(5)]
    public string Name { get; set; } = null!;

    [Required]
    public IFormFile ImageUrl { get; set; }
}
