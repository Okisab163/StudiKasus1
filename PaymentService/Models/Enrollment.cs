using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaymentService.Models
{
    public class Enrollment
    {
        [Key]
        [Required]
        public int EnrollmentID { get; set; }

        [Required]
        public int ExternatlID { get; set; }

        [Required]
        public string Name { get; set; }
        public ICollection<Payment> Payments { get; set; } =
            new List<Payment>();
    }
}
