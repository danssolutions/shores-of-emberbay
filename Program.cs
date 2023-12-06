using System.Security.Cryptography.X509Certificates;

namespace ShoresOfEmberbay
{
    public class Program
    {
        public const string QuitMessage = "Thank you for playing Shores of Emberbay!";
        
        public static void Main()
        {
            // Special code for handling a Ctrl+C press at any time the application is running.
            Console.CancelKeyPress += delegate
            {
                Console.Clear();
                Console.WriteLine(QuitMessage);
                Console.CursorVisible = true;
            };
            MainMenu menu = new();
            menu.Display();
        }
    }
}