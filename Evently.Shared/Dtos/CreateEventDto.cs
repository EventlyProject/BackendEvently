using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Shared.Dtos
{
    public class CreateEventDto
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public DateTime StartTime { get; set; }
        public string Details { get; set; }
        public string Location { get; set; }
        public int MaxParticipants { get; set; }
        public decimal? Price { get; set; }
        public string AccessRequirements { get; set; }
        public int CategoryId { get; set; }
    }
}
