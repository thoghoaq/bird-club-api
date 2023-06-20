using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdClubAPI.Domain.DTOs.View.Bird
{
    public class BirdViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Species { get; set; }
    }
}
