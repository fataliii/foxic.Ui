using System.ComponentModel.DataAnnotations;

namespace Foxic.Buisness.ViewModels.AreasViewModels.CategoryVM;

public class CategoryCreateVM
{
    [Required]
    public string CategoryName { get; set; } = null!;
}
