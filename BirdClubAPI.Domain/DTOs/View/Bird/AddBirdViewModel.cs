using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdClubAPI.Domain.DTOs.View.Bird
{
    public class AddBirdViewModel
    {
        public string Name { get; set; } = null!;
        public string? Species { get; set; }
        public int Id { get; set; }
    }
}
