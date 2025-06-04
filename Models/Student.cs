using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EducationSystem.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        // Навигационные свойства
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}