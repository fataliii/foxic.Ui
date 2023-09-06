using System.ComponentModel.DataAnnotations;

namespace Foxic.Buisness.ViewModels.AreasViewModels.SizeVM;

public class SizeCreateVM
{
    [Required]
    public string SizeName { get; set; } = null!;
}
