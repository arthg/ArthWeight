using System.ComponentModel.DataAnnotations;

namespace ArthWeight.ViewModels
{
    public class WeightViewModel
    {
        [Required]
        public decimal Weight { get; set; }
    }
}
