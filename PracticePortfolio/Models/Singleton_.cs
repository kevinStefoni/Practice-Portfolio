namespace PracticePortfolio.Models
{
    public class Singleton_
    {
        private static Singleton_? _Singleton = null;
        public int Value { get; set; }

        private Singleton_()
        {

        }

        public static Singleton_ GetInstance() => _Singleton ??= new ();

    }
}
