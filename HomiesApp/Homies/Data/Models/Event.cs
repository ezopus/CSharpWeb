namespace Homies.Data.Models
{

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Homies.Common.ValidationConstants.Event;
    public class Event
    {
        [Key]
        [Comment("The primary identifier of an event.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(EventNameMaxLength)]
        [Comment("The name of each event.")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionNameMaxLength)]
        [Comment("The description of each event up to symbol limit.")]
        public string Description { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Organiser))]
        [Comment("The unique id of each organiser of current event.")]
        public string OrganiserId { get; set; } = null!;

        public IdentityUser Organiser { get; set; } = null!;

        [Required]
        [Comment("The time on which the event was created.")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Comment("The time at which the event begins.")]
        public DateTime Start { get; set; }

        [Required]
        [Comment("The planned time at which the event will end.")]
        public DateTime End { get; set; }

        [Required]
        [Comment("The unique id of each type of event")]
        public int TypeId { get; set; }

        public Type Type { get; set; } = null!;

        public List<EventParticipant> EventsParticipants { get; set; } = new List<EventParticipant>();



    }
}
