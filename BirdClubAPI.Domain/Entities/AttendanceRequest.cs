using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdClubAPI.Domain.Entities
{
    public class AttendanceRequest
    {
        public int MemberId { get; set; }
        public int ActivityId { get; set; }
        public DateTime RequestTime { get; set; }

        public virtual Activity Activity { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
    }
}
