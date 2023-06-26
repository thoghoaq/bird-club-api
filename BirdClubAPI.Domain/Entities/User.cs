using System;
using System.Collections.Generic;

namespace BirdClubAPI.Domain.Entities
{
    public partial class User
    {
        public string Password { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public int Id { get; set; }
        public DateOnly? Birthday { get; set; }

        public virtual Member? Member { get; set; }
    }
}
