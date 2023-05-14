using System;
using System.Collections.Generic;

namespace BirdClubAPI.Domain.Entities
{
    public partial class Attendance
    {
        public int MemberId { get; set; }
        public int ActivityId { get; set; }
        public DateTime AttendanceTime { get; set; }

        public virtual Activity Activity { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
    }
}
