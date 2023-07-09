using BirdClubAPI.Domain.DTOs.Response.Newsfeed;
using BirdClubAPI.Domain.DTOs.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdClubAPI.Domain.DTOs.View.Member
{
    public class UserViewModel
    {
        public int Total { get; set; }
        public int Guest { get; set; }
        public int Member { get; set; }

      
    }
}
