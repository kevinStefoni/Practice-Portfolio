using System.Text.Json;

namespace PracticePortfolio.Models
{
    public class Singleton
    {
        private static Singleton? _Singleton = null;
        public int Value { get; set; }

        private Singleton()
        {

        }

        public static Singleton GetInstance() => _Singleton ??= new ();

    }
}
