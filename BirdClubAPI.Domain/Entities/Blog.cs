using System;
using System.Collections.Generic;

namespace BirdClubAPI.Domain.Entities
{
    public partial class Blog
    {
        public int NewsfeedId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }

        public virtual Newsfeed Newsfeed { get; set; } = null!;
    }
}
