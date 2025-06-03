using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Shared.Dtos
{
    // Data Transfer Object for Category entity
    // Used to transfer category data between layers (e.g., API and client)
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
