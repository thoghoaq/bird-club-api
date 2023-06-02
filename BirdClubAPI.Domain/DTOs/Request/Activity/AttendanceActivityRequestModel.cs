using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdClubAPI.Domain.DTOs.Request.Activity
{
    public class AttendanceActivityRequestModel
    {
       public DateTime AttendanceTime { get; set; }
        public int MemberId { get; set; } 
        public int ActivityId { get; set; } 
    }
}
