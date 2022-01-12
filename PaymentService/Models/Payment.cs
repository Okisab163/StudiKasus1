using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentService.Models
{
    public class Payment
    {
        [Key]
        [Required]
        public int PaymentID { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }
    }
}
