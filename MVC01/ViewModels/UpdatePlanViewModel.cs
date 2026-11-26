using System.ComponentModel.DataAnnotations;

namespace MVC01.ViewModels
{
    public class UpdatePlanViewModel
    {
        [Display(Name = "Plan")]
        public string PlanName { get; set; } = default!;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 200 chars")]
        public string Description { get; set; } = default!;

        [Required(ErrorMessage = "DurationDays is required")]
        [Range(1, 365, ErrorMessage = "DurationDays must be between 1 and 365 days")]
        [Display(Name = "Duration (days)")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 10000, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Price (EGP)")]
        public decimal Price { get; set; }
    }
}
