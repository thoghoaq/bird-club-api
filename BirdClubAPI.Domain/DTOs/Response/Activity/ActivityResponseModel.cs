﻿using BirdClubAPI.Domain.DTOs.Response.Member;

namespace BirdClubAPI.Domain.DTOs.Response.Activity
{
    public class ActivityResponseModel
    {
        public string Name { get; set; } = null!;
        public DateTime CreateTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string ActivityType { get; set; } = null!;
        public MemberResponseModel Owner { get; set; } = null!;
        public int Id { get; set; }
    }
}