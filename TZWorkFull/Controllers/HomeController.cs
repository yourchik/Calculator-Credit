using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using TZWorkFull.Models;

namespace TZWorkFull.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Privacy(Input input)
        {
            NumberFormatInfo test = new NumberFormatInfo()
            {
                NumberDecimalSeparator = "."
            };
            decimal interestRate;
            try
            {
                interestRate = Convert.ToDecimal(input.InterestRate, test);
            }
            catch (Exception)
            {
                interestRate = 0;
                ModelState.AddModelError("InterestRate", "Введите корректное значение");
            }
            if (ModelState.IsValid)
            {
                int month = 0;
                decimal monthlyRate = (interestRate / 12 / 100);
                decimal totalRate = (decimal)Math.Pow((double)(1 + monthlyRate), (double)input.CreditMonth);
                decimal monthlyPayment = (input.CreditSum * monthlyRate * totalRate) / (totalRate - 1);
                decimal balanceOwed = input.CreditSum;
                decimal overpayment = Math.Round((monthlyPayment * input.CreditMonth - input.CreditSum), 2);

                DateTime datanow = DateTime.Today;
                var datapay = datanow;
                var values = new List<OutputCalc>();

                for (int i = 0; i < input.CreditMonth; i++)
                {
                    decimal percentagePart = balanceOwed * monthlyRate;  //Процентная часть
                    decimal mainPart = monthlyPayment - percentagePart;  //Основная часть
                    balanceOwed -= mainPart;  // Остаток долга
                    month++;  //Месяца
                    datapay = datapay.AddMonths(1);  //Дата
                    var output = new OutputCalc
                    {
                        Date = datapay.ToString("d"),
                        Month = month,
                        MonthlyPayment = Math.Round(monthlyPayment, 2),
                        PercentagePart = Math.Round(percentagePart, 2),
                        MainPart = Math.Round(mainPart, 2),
                        BalanceOwed = Math.Round(balanceOwed, 2),
                    };
                    values.Add(output);
                }
                ViewBag.overpayment = overpayment;

                return View("/Views/Home/Privacy.cshtml", values);
            }
            return View("/Views/Home/Index.cshtml", input);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}