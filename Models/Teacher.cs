using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EducationSystem.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        // Навигационное свойство
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}