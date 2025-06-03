using System.ComponentModel.DataAnnotations;

namespace BackendEvently.Model
{
    // Represents a category for events
    public class Category
    {
        [Key] // Indicates that this property is the primary key for the Category entity
        public int Id { get; set; }

        [Required] // Ensures that the Name property must have a value
        public string Name { get; set; } = string.Empty;

        // Navigation property: contains all events that belong to this category
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
