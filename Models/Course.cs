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

        // ��������� ���� �� ������: Teacher -> Course
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        // ��������� ������ �� ������: Student <-> Course
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}