using System;
using System.Collections.Generic;

namespace BirdClubAPI.Domain.Entities
{
    public partial class Record
    {
        public int NewsfeedId { get; set; }
        public int BirdId { get; set; }
        public int Quantity { get; set; }
        public string? Photo { get; set; }

        public virtual Bird Bird { get; set; } = null!;
        public virtual Newsfeed Newsfeed { get; set; } = null!;
    }
}
