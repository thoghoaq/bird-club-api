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
            Newsfeeds = new HashSet<Newsfeed>();
        }

        public int UserId { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public bool MembershipStatus { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Newsfeed> Newsfeeds { get; set; }
    }
}
