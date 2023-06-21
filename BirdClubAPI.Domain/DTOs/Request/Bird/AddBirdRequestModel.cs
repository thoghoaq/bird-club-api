using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdClubAPI.Domain.DTOs.Request.Bird
{
    public class AddBirdRequestModel
    {
        public string Name { get; set; } = null!;
        public string? Species { get; set; }
    }
}
