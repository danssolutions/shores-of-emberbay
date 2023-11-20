namespace TownOfZuul
{
    public abstract class Menu
    {
        protected const string ActiveOption = "> ";
        protected const string InactiveOption = "  ";
        protected int selectedOption = 1;
        protected string[] options;
        protected bool continueDisplay = true;
        protected Menu()
        {
            options = Array.Empty<string>();
        }

        public virtual void Display()
        {
            //Console.Clear();
            //Console.WriteLine(this.Art);
            //Console.WriteLine(this.Text);

            while (continueDisplay)
            {
                for (int i = 1; i <= options.Length; i++)
                {
                    Console.Write((selectedOption == i ? ActiveOption : InactiveOption) + options[i - 1] + " (" + i + ")\n");
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
        private const string Art =
            @"
             _____                            __   _____           _ 
            |_   _|____      ___ __     ___  / _| |__  /   _ _   _| |
              | |/ _ \ \ /\ / / '_ \   / _ \| |_    / / | | | | | | |
              | | (_) \ V  V /| | | | | (_) |  _|  / /| |_| | |_| | |
              |_|\___/ \_/\_/ |_| |_|  \___/|_|   /____\__,_|\__,_|_|
                                                                     
            ---------------------------------------------------------
                                                                     
            ";

        private const string Text = "Use up/down arrow keys to select option, Enter or number keys to confirm, Esc to quit.\n";
        public MainMenu()
        {
            options = new string[] {
                "Play Game",
                //"Settings",
                "Credits",
                "Quit"
            };
        }

        override public void Display()
        {
            Console.Clear();

            Console.WriteLine(Art);
            Console.WriteLine(Text);

            base.Display();
        }

        override public void ParseOption(int option)
        {
            switch (option)
            {
                case 1:
                    StartGame();
                    break;
                /*case 2:
                    ShowSettings();
                    break;*/
                case 2:
                    ShowCredits();
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
            Console.WriteLine(Art);
            Console.WriteLine(Text);
        }

        private static void ShowSettings()
        {
            Console.Clear();
            Console.CursorVisible = true;

            //Docks docks = new();
            //FishingMenu fishMenu = new(docks,5);
            //fishMenu.Display();

            Console.Clear();
            Console.WriteLine(Art);
            Console.WriteLine(Text);
        }

        private static void ShowCredits()
        {
            Console.Clear();
            Console.CursorVisible = true;

            string Credits =
            "Town of Zuul was created as an SDU BSc Software Engineering project by:\n" +
            "- Bobike\n" +
            "- Condegall\n" +
            "- danssolutions\n" +
            "- Gierka\n" +
            "- Ivan\n" +
            "- perdita\n";

            GenericMenu credits = new(Art, Credits);
            credits.Display();

            Console.Clear();
            Console.WriteLine(Art);
            Console.WriteLine(Text);
        }

        private static void QuitGame()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine(Program.QuitMessage);
            Environment.Exit(0);
        }
    }

    public class GenericMenu : Menu // a menu with no options except 'press any key to continue' prompt, with customizable art and text
    {
        private readonly string Art, Text;
        public GenericMenu(string art, string text)
        {
            this.Art = art;
            this.Text = text;
        }

        override public void Display()
        {
            Console.Clear();
            Console.WriteLine(Art);
            Console.WriteLine(Text);
            Console.WriteLine("\nPress any key to continue.\n");
            ConsoleKey key = Console.ReadKey(true).Key;
        }
    }

    public sealed class FishingMenu : Menu
    {
        private const string AssignedVillagersInfo = "Villagers ready to fish: ";
        private const string FreeVillagersInfo = "Villagers waiting for assignment: ";
        private const string AssignedOptionInfo = " will be fishing for ";
        private readonly uint totalVillagers;
        private uint freeVillagers;
        private bool confirmed = false;

        private readonly List<Fish> fishList = new();
        public readonly List<uint> fisherList = new();

        public FishingMenu(FishableLocation location, uint amount)
        {
            totalVillagers = freeVillagers = amount;

            fishList = location.LocalFish;

            for (int i = 0; i < fishList.Count; i++)
                fisherList.Add(0);

            // fish marked as "bycatch only" cannot be assigned to villagers and won't show up here
            if (fishList.Count > 0)
                options = fishList.Where(fish => fish.BycatchOnly == false).Select(fish => fish.Name ?? "").ToArray();
        }

        override public void Display()
        {
            Console.Clear();

            Console.WriteLine("~~~ Fishing time! ~~~");
            Console.Write(totalVillagers);
            Console.WriteLine(" villagers in total have been assigned to fish in this location. " +
            "Choose which type of fish each villager should try to catch.\n" +
            "Use up/down arrow keys to select option, left/right arrow keys to change villager amounts, Enter to confirm.\n");

            while (continueDisplay)
            {
                Console.WriteLine(FreeVillagersInfo + freeVillagers + "".PadRight(freeVillagers.ToString().Length) + "\n");

                uint assignedVillagers = totalVillagers - freeVillagers;
                Console.WriteLine(AssignedVillagersInfo + assignedVillagers + "".PadRight(assignedVillagers.ToString().Length));
                for (int i = 1; i <= options.Length; i++)
                {
                    Console.Write((selectedOption == i ? ActiveOption : InactiveOption) + fisherList[i - 1] + AssignedOptionInfo +
                    options[i - 1] + " (" + fishList[i - 1].GetCatchDifficultyString() + " difficulty)" +
                    "".PadRight(fisherList[i - 1].ToString().Length) + "\n");
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
            if (options.Length < option)
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
            Console.WriteLine("Assignment cancelled.");
        }

        public void ConfirmAssignment()
        {
            confirmed = true;
            continueDisplay = false;

            Console.Clear();

            Console.WriteLine("Assignment confirmed.\n");
            Console.WriteLine(AssignedVillagersInfo + (totalVillagers - freeVillagers));
            for (int i = 1; i <= options.Length; i++)
                Console.Write(fisherList[i - 1] + AssignedOptionInfo + options[i - 1] + ".\n");

            Console.WriteLine("\n");
        }

        public List<uint> GetFisherList(List<uint> existingFishers)
        {
            return confirmed ? fisherList : existingFishers;
        }
    }
    
    public sealed class DialogueMenu : Menu
    {
        private Location? currentLocation;
        private readonly Stack<Location> previousLocations = new();
        private const string DialogueInstructions = "Chose one of the options to interact with the character in the location";
        private const string more = "Keep talking with the character";
        private const string stop = "Stop if you have heard enough, and wish to move on";
        private const string who = "Learn more about how the character can help";
        private const string items = "Ask what items you can unlock and how";
        private const string unlock = "Unlock item";

        private bool unlocked = false;
        private bool continueDisplay = true;

        public DialogueMenu()
        {
            options = new string[] {
                more,
                stop,
                who,
                items,
                unlock
            };
        }

        override public void Display()
        {
            Console.Clear();

            Console.WriteLine(DialogueInstructions);

            base.Display();
        }

        override public void ParseOption(int option)
        {
            switch (option)
            {
                case 1:
                    moreOption();
                    break;
                case 2:
                    stopOption();
                    break;
                case 3:
                    whoOption();
                    break;
                case 4:
                    itemsOption();
                    break;
                case 5:
                    unlockOption();
                    break;
            }
        }

            private static void moreOption()
            {
                Console.Clear();
                Console.CursorVisible = true;

                characterMenu npcs = new();
                npcs.Display();

                Console.Clear();

                //Console.WriteLine("you chose more");
 //               Console.WriteLine(currentLocation?.Description);
            }

            private static void stopOption()
            {
                Console.Clear();
                Console.CursorVisible = true;

                Console.Clear();

                Console.WriteLine("You chose stop");
            }

            private static void whoOption()
            {
                Console.Clear();
                Console.CursorVisible = true;

                Console.Clear();

                Console.WriteLine("You chose who");
            }

            private static void itemsOption()
            {
                Console.Clear();
                Console.CursorVisible = true;

                Console.Clear();                
                Console.WriteLine("You chose items");
            }

            private static void unlockOption()
            {
                Console.Clear();
                Console.CursorVisible = true;

                Console.Clear();                
                Console.WriteLine("You chose unlock.");

                //Create object/instance of a class
            }
    }

    public sealed class characterMenu : Menu
    {
        private Location? currentLocation;
        //public character? character;

        private const string characterLogo = 
            @"
                                        
             __      __  ___  __      __ 
             \ \ /\ / / / _ \ \ \ /\ / / 
              \ V  V / | (_) | \ V  V /
               \_/\_/   \___/   \_/\_/  
                                                                     
            ---------------------------------------------------------
                                                                     
            ";
        
        private const string option1 = 
            "You are talking to a character";

        //private const string option2 = 
          //  "You are speaking with a character";

        override public void Display()
        {
            Console.Clear();
            
            Console.WriteLine(characterLogo);
            Console.WriteLine(option1);

            ConsoleKey key = Console.ReadKey(true).Key;
        }
    }
    public class EndingMenu : Menu
    {
        public bool StopGame { get; private set; } = false;
        private const string Art = @"


                       Ending :)
         I am placeholder art, replace me!
            
                                                                     
---------------------------------------------------------
                                                                     
            ";

        private const string Text = "Would you like to continue playing?\n";

        public EndingMenu()
        {
            options = new string[] {
                "Yes (Continue Playing)",
                "No (Go to Main Menu)"
            };
        }

        override public void Display()
        {
            Console.Clear();

            Console.WriteLine(Art);
            Console.WriteLine(Text);

            base.Display();
        }

        override public void ParseOption(int option)
        {
            switch (option)
            {
                case 1:
                    StopGame = false;
                    continueDisplay = false;
                    break;
                case 2:
                    StopGame = true;
                    continueDisplay = false;
                    break;
            }
        }
    }
}
