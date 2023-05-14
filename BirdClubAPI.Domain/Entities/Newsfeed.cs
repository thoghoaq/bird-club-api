using System;
using System.Collections.Generic;

namespace BirdClubAPI.Domain.Entities
{
    public partial class Newsfeed
    {
        public DateTime PublicationTime { get; set; }
        public int OwnerId { get; set; }
        public int Id { get; set; }

        public virtual Member Owner { get; set; } = null!;
        public virtual Blog? Blog { get; set; }
        public virtual Record? Record { get; set; }
    }
}
