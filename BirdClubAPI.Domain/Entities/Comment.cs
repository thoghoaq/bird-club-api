using System;
using System.Collections.Generic;

namespace BirdClubAPI.Domain.Entities
{
    public partial class Comment
    {
        public string Content { get; set; } = null!;
        public DateTime PublicationTime { get; set; }
        public int OwnerId { get; set; }
        public string Type { get; set; } = null!;
        public int ReferenceId { get; set; }
        public int Id { get; set; }

        public virtual Member Owner { get; set; } = null!;
        public virtual Activity Reference { get; set; } = null!;
        public virtual Record Reference1 { get; set; } = null!;
        public virtual Blog ReferenceNavigation { get; set; } = null!;
    }
}
