using System.ComponentModel.DataAnnotations;

namespace PaymentService.Dtos
{
    public class EnrollmentForCreateDto
    {
        [Required]
        public int ExternatlID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
