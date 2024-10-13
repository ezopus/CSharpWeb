namespace Homies.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Homies.Common.ValidationConstants.Event;
    public class EventAddViewModel
    {
        //Name, Description, Start, End, TypeId, CreatedOn, Types

        [Required]
        [StringLength(EventNameMaxLength, MinimumLength = EventNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionNameMaxLength, MinimumLength = DescriptionNameMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string Start { get; set; }

        [Required]
        public string End { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public int TypeId { get; set; }

        public IList<Data.Models.Type> Types { get; set; } = null!;

    }
}
