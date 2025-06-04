using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EducationSystem.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        // Отношение один ко многим: Teacher -> Course
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        // Отношение многие ко многим: Student <-> Course
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}