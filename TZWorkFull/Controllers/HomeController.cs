using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
            if (ModelState.IsValid)
            {
                int Month = 0;
                decimal creditSum = input.CreditSum;  //Сумма кредита
                int creditMonth = input.CreditMonth;  //Кол-во месяцев
                decimal interestRate = Convert.ToDecimal(input.InterestRate.Replace(".", ","));  //Ставка

                decimal MonthlyRate = interestRate / 12 / 100;
                decimal TotalRate = (decimal)Math.Pow((double)(1 + MonthlyRate), (double)creditMonth);
                decimal MonthlyPayment = (creditSum * MonthlyRate * TotalRate) / (TotalRate - 1);
                decimal BalanceOwed = creditSum;
                decimal Overpayment = MonthlyPayment * creditMonth - creditSum;

                DateTime datanow = DateTime.Today;
                var datapay = datanow;

                var values = new List<OutputCalc>();

                for (int i = 0; i < creditMonth; i++)
                {
                    decimal PercentagePart = BalanceOwed * MonthlyRate;  //Процентная часть
                    decimal MainPart = MonthlyPayment - PercentagePart;  //Основная часть
                    BalanceOwed -= MainPart;  // Остаток долга
                    Month++;  //Месяца
                    datapay = datapay.AddMonths(1);  //Дата
                    var output = new OutputCalc
                    {
                        Date = datapay.ToString("d"),
                        Month = Month,
                        MonthlyPayment = Math.Round(MonthlyPayment, 2),
                        PercentagePart = Math.Round(PercentagePart, 2),
                        MainPart = Math.Round(MainPart, 2),
                        BalanceOwed = Math.Round(BalanceOwed, 2)
                    };
                    values.Add(output);
                }
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