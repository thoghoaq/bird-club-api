using System;
using System.Collections.Generic;

namespace BirdClubAPI.Domain.Entities
{
    public partial class Feedback
    {
        public int OwnerId { get; set; }
        public int ActivityId { get; set; }
        public DateTime Time { get; set; }
        public string? Content { get; set; }
        public int Rating { get; set; }
        public int Id { get; set; }

        public virtual Activity Activity { get; set; } = null!;
        public virtual Member Owner { get; set; } = null!;
    }
}
