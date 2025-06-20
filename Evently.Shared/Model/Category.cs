﻿using System.ComponentModel.DataAnnotations;

namespace BackendEvently.Model
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
