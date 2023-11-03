namespace TownOfZuul
{
    public abstract class Menu
    {
        protected const string ActiveOption = "> ";
        protected const string InactiveOption = "  ";
        protected int selectedOption = 1;
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
                            selectedOption--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedOption < options.Length)
                            selectedOption++;
                        break;

                    case ConsoleKey.Enter:
                        ParseOption(selectedOption);
                        break;

                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                    case ConsoleKey.D6:
                    case ConsoleKey.D7:
                    case ConsoleKey.D8:
                    case ConsoleKey.D9:
                        ParseOption((int)key - 48);
                        break;
                    
                    case ConsoleKey.Escape:
                        ParseEscapeOption();
                        break;
                }
            }
        }

        public virtual void ParseOption(int option)
        {
             // Intentionally left empty. This method must be overridden in classes inherited from Menu.
        }

        public virtual void ParseEscapeOption()
        {
            // Intentionally left empty. This method must be overridden in classes inherited from Menu.
        }
    }

    public sealed class MainMenu : Menu
    {
        private const string Logo = 
            @"
             _____                            __   _____           _ 
            |_   _|____      ___ __     ___  / _| |__  /   _ _   _| |
              | |/ _ \ \ /\ / / '_ \   / _ \| |_    / / | | | | | | |
              | | (_) \ V  V /| | | | | (_) |  _|  / /| |_| | |_| | |
              |_|\___/ \_/\_/ |_| |_|  \___/|_|   /____\__,_|\__,_|_|
                                                                     
            ---------------------------------------------------------
                                                                     
            ";
        
        private const string Instructions = "Use up/down arrow keys to select option, Enter or number keys to confirm, Esc to quit.\n";
        private const string PlayOption = "Play Game";
        private const string SettingsOption = "Settings";
        private const string CreditsOption = "Credits";
        private const string QuitOption = "Quit";

        private const string QuitMessage = "Thank you for playing Town Of Zuul!";
        
        public MainMenu()
        {
            options = new string[] {
                PlayOption,
                SettingsOption,
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
        
        override public void ParseOption(int option)
        {
            switch (option)
            {
                case 1:
                    StartGame();
                    break;
                case 2:
                    ShowSettings();
                    break;
                case 3:
                    ShowCredits();
                    break;
                case 4:
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

        private static void ShowSettings()
        {
            Console.Clear();
            Console.CursorVisible = true;

            Docks docks = new();
            FishingMenu fishMenu = new(docks,5);
            fishMenu.Display();

            Console.Clear();
            Console.WriteLine(Logo);
            Console.WriteLine(Instructions);
        }

        private static void ShowCredits()
        {
            Console.Clear();
            Console.CursorVisible = true;

            CreditsMenu credits = new();
            credits.Display();

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

    public sealed class CreditsMenu : Menu
    {
        private const string Logo = 
            @"
             _____                            __   _____           _ 
            |_   _|____      ___ __     ___  / _| |__  /   _ _   _| |
              | |/ _ \ \ /\ / / '_ \   / _ \| |_    / / | | | | | | |
              | | (_) \ V  V /| | | | | (_) |  _|  / /| |_| | |_| | |
              |_|\___/ \_/\_/ |_| |_|  \___/|_|   /____\__,_|\__,_|_|
                                                                     
            ---------------------------------------------------------
                                                                     
            ";
        
        private const string Credits = 
            "Town of Zuul was created as an SDU BSc Software Engineering project by:\n" +
            "- Bobike\n" +
            "- Condegall\n" +
            "- danssolutions\n" +
            "- Gierka\n" +
            "- Ivan\n" +
            "- perdita\n" +
            "\nPress any key to return to the main menu.\n";

        override public void Display()
        {
            Console.Clear();
            
            Console.WriteLine(Logo);
            Console.WriteLine(Credits);

            ConsoleKey key = Console.ReadKey(true).Key;
        }
    }

    public class FishingMenu : Menu
    {
        private const string Intro = "~~~ Fishing time! ~~~";
        private const string Instructions = " villagers in total have been assigned to fish in this location. " + 
            "Choose which type of fish each villager should try to catch.\n" +
            "Use up/down arrow keys to select option, left/right arrow keys to change villager amounts, Enter to confirm.\n";
        
        private const string AssignedVillagersInfo = "Villagers ready to fish: ";
        private const string FreeVillagersInfo = "Villagers waiting for assignment: ";
        private const string AssignedOptionInfo = " will be fishing for ";
        private const string ConfirmedOutro = "Assignment confirmed.";
        private const string CancelledOutro = "Assignment cancelled.";

        private readonly uint totalVillagers;
        private uint freeVillagers;
        private bool continueDisplay = true;
        private bool confirmed = false;
        
        private readonly List<Fish> fishList = new();
        public readonly List<uint> fisherList = new();
        
        
        public FishingMenu(FishableLocation location, uint amount)
        {
            totalVillagers = freeVillagers = amount;
            
            fishList = location.LocalFish;
            fishList.RemoveAll(fish => fish.BycatchOnly == true); // fish marked as "bycatch only" cannot be assigned to villagers and won't show up here

            if (fishList.Count > 0)
                options = fishList.Select(fish => fish.Name ?? "").ToArray();
            
            for (int i = 0; i < fishList.Count; i++)
                fisherList.Add(0);
        }
        
        override public void Display()
        {
            Console.Clear();
            
            Console.WriteLine(Intro);
            Console.Write(totalVillagers);
            Console.WriteLine(Instructions);

            while (continueDisplay)
            {
                Console.WriteLine(FreeVillagersInfo + freeVillagers + "".PadRight(freeVillagers.ToString().Length) + "\n");

                uint assignedVillagers = totalVillagers - freeVillagers;
                Console.WriteLine(AssignedVillagersInfo + assignedVillagers + "".PadRight(assignedVillagers.ToString().Length));
                for (int i = 1; i <= options.Length; i++)
                {
                    Console.Write((selectedOption == i ? ActiveOption : InactiveOption) + fisherList[i-1] + AssignedOptionInfo + options[i-1] +
                        "." + "".PadRight(fisherList[i-1].ToString().Length) + "\n");
                }
                
                ConsoleKey key = Console.ReadKey(true).Key;

                Console.SetCursorPosition(0, Console.CursorTop - options.Length - 3);
                Console.CursorVisible = false;
                
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedOption > 1)
                            selectedOption--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedOption < options.Length)
                            selectedOption++;
                        break;
                    
                    case ConsoleKey.LeftArrow:
                        if (fisherList[selectedOption - 1] > 0)
                        {
                            fisherList[selectedOption - 1]--;
                            freeVillagers++;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        ParseOption(selectedOption);
                        break;

                    case ConsoleKey.Enter:
                        ConfirmAssignment();
                        break;

                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                    case ConsoleKey.D6:
                    case ConsoleKey.D7:
                    case ConsoleKey.D8:
                    case ConsoleKey.D9:
                        ParseOption((int)key - 48);
                        break;
                    
                    case ConsoleKey.Escape:
                        ParseEscapeOption();
                        break;
                }
            }

            Console.CursorVisible = true;
        }

        override public void ParseOption(int option)
        {
            if (fisherList.Count < option)
                return;
            
            if (freeVillagers > 0)
            {
                fisherList[option - 1]++;
                freeVillagers--;
            }
        }

        override public void ParseEscapeOption()
        {
            confirmed = false;
            continueDisplay = false;

            Console.Clear();
            Console.WriteLine(CancelledOutro);
        }

        public void ConfirmAssignment()
        {
            confirmed = true;
            continueDisplay = false;

            Console.Clear();

            Console.WriteLine(ConfirmedOutro + "\n");
            Console.WriteLine(AssignedVillagersInfo + (totalVillagers - freeVillagers));
            for (int i = 1; i <= options.Length; i++)
                Console.Write(fisherList[i-1] + AssignedOptionInfo + options[i-1] + ".\n");
            
            Console.WriteLine("\n");
        }

        public List<uint> GetFisherList(List<uint> existingFishers)
        {
            return confirmed ? fisherList : existingFishers;
        }
    }
}
