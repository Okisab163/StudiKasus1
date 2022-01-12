using System;

namespace PaymentService.Dtos
{
    public class PaymentDto
    {
        public int PaymentID { get; set; }
        public int Price { get; set; }
        public DateTime PaymentDate { get; set; }
        public int EnrollmentId { get; set; }
    }
}
