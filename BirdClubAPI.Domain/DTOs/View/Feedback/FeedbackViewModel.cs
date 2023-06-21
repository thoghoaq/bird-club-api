using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdClubAPI.Domain.DTOs.View.Feedback
{
    public class FeedbackViewModel
    {
        public int OwnerId { get; set; }
        public int ActivityId { get; set; }
        public DateTime Time { get; set; }
        public string? Content { get; set; }
        public int Rating { get; set; }
        public int Id { get; set; }
    }
}
