using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaymentService.Models
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
        [Required]
        public Grade Grade { get; set; }
        public ICollection<Payment> Payments { get; set; } =
            new List<Payment>();
    }
}
