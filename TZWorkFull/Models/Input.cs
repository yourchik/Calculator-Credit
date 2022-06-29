using System.ComponentModel.DataAnnotations;

namespace TZWorkFull.Models
{
    public class Input
    {
        [Required(ErrorMessage = "Введите сумму кредита")]
        [Range(1, 100000000000, ErrorMessage = "Недопустимый срок")]
        public decimal CreditSum { get; set; }
        [Range(1, 360, ErrorMessage = "Недопустимый срок")]
        [Required(ErrorMessage = "Введите срок кредита")]
        public int CreditMonth { get; set; }
        [Required(ErrorMessage = "Введите процентную ставку")]
        public string InterestRate { get; set; }
    }
}
