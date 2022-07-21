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
        public string InterestRate { get; set; } = null!;
        [Required(ErrorMessage = "Введите имя")]
        [RegularExpression(@"[А-Я]{1}[а-я]{1,10}", ErrorMessage = "Некорректное имя")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Введите фамилию")]
        [RegularExpression("[А-Я]{1}[а-я]{1,10}", ErrorMessage = "Некорректная фамилия")]
        public string SecondName { get; set; } = null!;
        [Required(ErrorMessage = "Введите номер телефона")]
        [RegularExpression(@"[8]{1}[9]{1}[0-9]{9}", ErrorMessage = "Некорректный номер телефона")]
        public string Number { get; set; } = null!;
        public int Id { get; set; }

    }
}
