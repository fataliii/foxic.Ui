using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Foxic.Buisness.ViewModels.AreasViewModels.ColorVM;

public class ColorCreateVM
{
    [Required]
    public string ColorName { get; set; } = null!;
    [Required]
    public IFormFile ImageUrl { get; set; }
}
