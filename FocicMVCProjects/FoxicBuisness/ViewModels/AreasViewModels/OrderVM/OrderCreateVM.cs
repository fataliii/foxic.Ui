using System.ComponentModel.DataAnnotations;

namespace Foxic.Buisness.ViewModels.AreasViewModels.OrderVM;

public class OrderCreateVM
{
    [Required]
    public double TotalPrice { get; set; }
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public DateTime CreatedTime { get; set; }
}
