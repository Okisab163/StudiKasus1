using System.ComponentModel.DataAnnotations;

namespace EnrollmentService.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        [Key]
        [Required]
        public int EnrollmentID { get; set; }

        [Required]
        public int CourseID { get; set; }

        [Required]
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
