﻿namespace Homies.Models
{
    public class EventInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Start { get; set; }
        public string End { get; set; }

        public string Type { get; set; }
        public int TypeId { get; set; }

        public string Description { get; set; }

        public string Organiser { get; set; }

        public ICollection<TypeViewModel> Types { get; set; }
    }
}
