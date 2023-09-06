using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Foxic.Buisness.ViewModels.AreasViewModels.CollectionVM;

public class CollectionCreateVM
{
    [Required]
    public string CollectionName { get; set; } = null!;
    [Required]
    public IFormFile ImageUrl { get; set; }
}
