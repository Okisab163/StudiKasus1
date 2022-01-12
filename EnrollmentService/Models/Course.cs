using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentService.Models
{
    public class Course
    {
        [Key]
        [Required]
        public int CourseID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
