using System;
using System.Collections.Generic;

namespace BirdClubAPI.Domain.Entities
{
    public partial class Activity
    {
        public Activity()
        {
            Attendances = new HashSet<Attendance>();
        }

        public string Name { get; set; } = null!;
        public DateTime CreateTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string ActivityType { get; set; } = null!;
        public int OwnerId { get; set; }
        public int Id { get; set; }
        public bool Status { get; set; }

        public virtual Member Owner { get; set; } = null!;
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
