using System;
using System.Collections.Generic;

namespace BirdClubAPI.Domain.Entities
{
    public partial class Activity
    {
        public Activity()
        {
            Attendances = new HashSet<Attendance>();
            Comments = new HashSet<Comment>();
            Feedbacks = new HashSet<Feedback>();
            Likes = new HashSet<Like>();
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
        public bool? Status { get; set; }
        public string? Background { get; set; }

        public virtual Member Owner { get; set; } = null!;
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
