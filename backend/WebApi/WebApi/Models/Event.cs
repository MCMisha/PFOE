using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Category { get; set; } = null!;
        public DateOnly Date { get; set; }
        public int ParticipantNumber { get; set; }
        public int Organizer { get; set; }
        public int VisitsNumber { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
