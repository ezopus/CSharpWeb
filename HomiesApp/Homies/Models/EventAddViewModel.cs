namespace Homies.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Homies.Common.ValidationConstants.Event;
    public class EventAddViewModel
    {
        [Required]
        [StringLength(EventNameMaxLength, MinimumLength = EventNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionNameMaxLength, MinimumLength = DescriptionNameMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string Start { get; set; } = string.Empty;

        [Required]
        public string End { get; set; } = string.Empty;

        [Required]
        public int TypeId { get; set; }

        public IEnumerable<TypeViewModel> Types { get; set; } = new List<TypeViewModel>();

    }
}
