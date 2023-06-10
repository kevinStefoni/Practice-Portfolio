namespace PracticePortfolio.Models.DTOs
{
    public class WrapMethodPayload
    {
        public string Name { get; set; } = string.Empty;
        public decimal PayRate { get; set; }
        public IList<int>[] DailyHoursWorked { get; set; }
    }
}
