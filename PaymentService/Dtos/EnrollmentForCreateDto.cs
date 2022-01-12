using System.ComponentModel.DataAnnotations;

namespace PaymentService.Dtos
{
    public class EnrollmentForCreateDto
    {
        [Required]
        public int CourseID { get; set; }
        [Required]
        public int StudentID { get; set; }
        [Required]
        public Grade Grade { get; set; }
    }
}
