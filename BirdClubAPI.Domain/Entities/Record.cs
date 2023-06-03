using System;
using System.Collections.Generic;

namespace BirdClubAPI.Domain.Entities
{
    public partial class Record
    {
        public Record()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
        }

        public int NewsfeedId { get; set; }
        public int BirdId { get; set; }
        public int Quantity { get; set; }
        public string? Photo { get; set; }

        public virtual Bird Bird { get; set; } = null!;
        public virtual Newsfeed Newsfeed { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
