using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdClubAPI.Domain.DTOs.Request.Newsfeed
{
    public class NewsFeedRequestModel
    {
        public int MemberId { get; set; }
        public int NewsFeedId { get; set; }
    }
}
