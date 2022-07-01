namespace TZWorkFull.Models
{
    public class OutputCalc
    {
        public object Date { get; set; }
        public int Month { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal PercentagePart { get; set; }
        public decimal MainPart { get; set; }
        public decimal BalanceOwed { get; set; }
    }
}
