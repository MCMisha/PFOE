using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Participant
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
    }
}
