using System;
using System.Collections.Generic;

namespace BirdClubAPI.Domain.Entities
{
    public partial class Member
    {
        public Member()
        {
            Activities = new HashSet<Activity>();
            Attendances = new HashSet<Attendance>();
            Comments = new HashSet<Comment>();
            Feedbacks = new HashSet<Feedback>();
            Likes = new HashSet<Like>();
            Newsfeeds = new HashSet<Newsfeed>();
        }

        public int UserId { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public bool MembershipStatus { get; set; }
        public DateOnly? Birthday { get; set; }
        public string? About { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Newsfeed> Newsfeeds { get; set; }
    }
}
