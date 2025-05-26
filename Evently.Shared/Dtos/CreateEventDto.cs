using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Shared.Dtos
{
    public class CreateEventDto
    {
        public string Name { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public string Details { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int MaxParticipants { get; set; }
        public decimal? Price { get; set; }
        public string AccessRequirements { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}
