using System.ComponentModel.DataAnnotations;

namespace ArthWeight.ViewModels
{
    public sealed class WeightViewModel
    {
        [Required]
        [Range(.01, 500)]
        public decimal Weight { get; set; }
    }
}
