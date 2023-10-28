namespace TownOfZuul
{
    public abstract class Menu
    {
        protected const string ActiveOption = "> ";
        protected const string InactiveOption = "  ";
        protected int selectedOption = 1;
        protected int previousSelectedOption = 1;
        protected string[] options;
        protected Menu()
        {
            options = Array.Empty<string>();
        }

        public virtual void Display()
        {
            while (true)
            {
                for (int i = 1; i <= options.Length; i++)
                {
                    Console.Write((selectedOption == i ? ActiveOption : InactiveOption) + options[i-1] + " (" + i + ")\n");
                }
                
                ConsoleKey key = Console.ReadKey(true).Key;

                Console.SetCursorPosition(0, Console.CursorTop - options.Length);
                Console.CursorVisible = false;
                
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedOption > 1)
                        {
                            previousSelectedOption = selectedOption;
                            selectedOption--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedOption < options.Length)
                        {
                            previousSelectedOption = selectedOption;
                            selectedOption++;
                        }
                        break;

                    case ConsoleKey.Enter:
                        ParseOption(selectedOption);
                        break;

                    case ConsoleKey.D1:
                        ParseOption(1);
                        break;
                    case ConsoleKey.D2:
                        ParseOption(2);
                        break;
                    case ConsoleKey.D3:
                        ParseOption(3);
                        break;
                    case ConsoleKey.D4:
                        ParseOption(4);
                        break;
                    case ConsoleKey.D5:
                        ParseOption(5);
                        break;
                    case ConsoleKey.D6:
                        ParseOption(6);
                        break;
                    case ConsoleKey.D7:
                        ParseOption(7);
                        break;
                    case ConsoleKey.D8:
                        ParseOption(8);
                        break;
                    case ConsoleKey.D9:
                        ParseOption(9);
                        break;
                    case ConsoleKey.D0:
                        ParseOption(10);
                        break;
                    
                    case ConsoleKey.Escape:
                        ParseEscapeOption();
                        break;
                }
            }
        }

        public virtual void ParseOption(int option)
        {
            if (option <= options.Length)
                return;
        }

        public virtual void ParseEscapeOption()
        {
            // Intentionally left empty. This method must be overridden in classes inherited from Menu.
        }
    }

    public class MainMenu : Menu
    {
        private const string Logo = 
            @" _____                            __   _____           _ " + "\n" +
            @"|_   _|____      ___ __     ___  / _| |__  /   _ _   _| |" + "\n" +
            @"  | |/ _ \ \ /\ / / '_ \   / _ \| |_    / / | | | | | | |" + "\n" +
            @"  | | (_) \ V  V /| | | | | (_) |  _|  / /| |_| | |_| | |" + "\n" +
            @"  |_|\___/ \_/\_/ |_| |_|  \___/|_|   /____\__,_|\__,_|_|" + "\n" +
            @"                                                         " + "\n" +
            @"---------------------------------------------------------" + "\n" +
            @"                                                         " + "\n";
        
        private const string Instructions = "Use up/down arrow keys or number keys to select option, Enter to confirm, Esc to quit.\n";
        private const string PlayOption = "Play Game";
        private const string CreditsOption = "Credits";
        private const string QuitOption = "Quit";

        private const string QuitMessage = "Thank you for playing Town Of Zuul!";
        
        public MainMenu()
        {
            options = new string[] {
                PlayOption,
                CreditsOption,
                QuitOption
            };
        }

        override public void Display()
        {
            Console.Clear();
            
            Console.WriteLine(Logo);
            Console.WriteLine(Instructions);

            base.Display();
        }

        /*enum MainMenuOption : int
        {

        }*/

        override public void ParseOption(int option)
        {
            base.ParseOption(option);

            switch (option)
            {
                case 1:
                    StartGame();
                    break;
                case 2:
                    Console.WriteLine("Credits: made by us. :)");
                    break;
                case 3:
                    QuitGame();
                    break;
            }
        }

        override public void ParseEscapeOption()
        {
            QuitGame();
        }

        private static void StartGame()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Game game = new();
            game.Play();

            Console.Clear();
            Console.WriteLine(Logo);
            Console.WriteLine(Instructions);
        }

        private static void QuitGame()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine(QuitMessage);
            Environment.Exit(0);
        }
    }

    public class FishingMenu : Menu
    {
        // this should take in an IFishable location
        // then fetch all the fish types that location has

        // also this should know how many villagers are being assigned to the location

        // in the end, this should set all necessary vars for game state and frick off afterwards
    }
}
