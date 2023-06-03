namespace PracticePortfolio.Models
{
    public class ImperialPound
    {
        private const double KilogramToImperialPoundConversionFactor = 2.20462;
        private double _pounds;

        public double Pounds { 
            get { return _pounds; }
            private set { _pounds = value; }
        }

        public ImperialPound(double pounds)
        {
            Pounds = pounds;
        }

        public static explicit operator ImperialPound(double value) => new (KilogramToImperialPoundConversionFactor * value);

    }
}
