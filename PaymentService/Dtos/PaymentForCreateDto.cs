using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentService.Dtos
{
    public class PaymentForCreateDto
    {
        [Required]
        public int Price { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
    }
}
