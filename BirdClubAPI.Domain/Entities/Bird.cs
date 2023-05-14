using System;
using System.Collections.Generic;

namespace BirdClubAPI.Domain.Entities
{
    public partial class Bird
    {
        public Bird()
        {
            Records = new HashSet<Record>();
        }

        public string Name { get; set; } = null!;
        public string? Species { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Record> Records { get; set; }
    }
}
