using System;
using System.Collections.Generic;

namespace BirdClubAPI.Domain.Entities
{
    public partial class Blog
    {
        public Blog()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
        }

        public int NewsfeedId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }

        public virtual Newsfeed Newsfeed { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
