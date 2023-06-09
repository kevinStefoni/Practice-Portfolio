namespace PracticePortfolio.Models.DTOs
{
    public class WrapMethodPayload
    {
        public decimal PayRate { get; set; }
        public IList<int>[] DailyHoursWorked { get; set; }
    }
}
