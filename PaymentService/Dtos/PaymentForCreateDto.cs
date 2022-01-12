using System.ComponentModel.DataAnnotations;

namespace PaymentService.Dtos
{
    public class PaymentForCreateDto
    {
        [Required]
        public string HowTo { get; set; }
        [Required]
        public string CommandLine { get; set; }
    }
}
