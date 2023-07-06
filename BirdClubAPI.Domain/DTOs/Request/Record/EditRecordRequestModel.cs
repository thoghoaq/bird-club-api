using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdClubAPI.Domain.DTOs.Request.Record
{
    public class EditRecordRequestModel
    {
        public int BirdId { get; set; }
        public int Quantity { get; set; }
        public string? Photo { get; set; }
    }
}
