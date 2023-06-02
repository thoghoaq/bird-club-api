using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdClubAPI.Domain.DTOs.View.Acitivity
{
    public class AttendanceActivityViewModel
    {
        public string AttendanceId { get; set; } = null!;
        public string MemberID { get; set; } = null!;
        public DateTime AttendanceTime { get; set; }
    }
}
