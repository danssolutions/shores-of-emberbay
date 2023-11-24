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
        private const string Art = GameArt.MenuLogo;
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

            // Play intro slides
            string villageText =
            "The village of Emberbay is a remote settlement next to the ocean. \nIt used to be a sizable trading hub a few decades ago, \nbut due to excessive pollution and unsustainable development by local industries, \nthe village suffers from food shortages and overall poor health among the population to this day.";
            string wildlifeText = 
            "Up until now, no attempts to restore the village have been made due to lack of interest and funding. \nHowever, according to latest reports from the on-site FRV, \nthe local marine environment is on the verge of irreversible decline and eventual extinction \ndue to extreme pollution and the actions of the local villagers in the past. \nThis is an unacceptable course of events which must be resolved immediately.";
            string mayorText =
            "You have been appointed as the new mayor of the village of Emberbay. \nYour task is to restore the village to a state where it can be self-sufficient and develop sustainably in the span of 12 months. \nTo ensure this, the village should have a population of at least 400 and at least 95% of the population should be healthy.";
            GenericMenu villageSlide = new(GameArt.Village, villageText);
            villageSlide.Display();
            GenericMenu wildlifeSlide = new(GameArt.ResearchVessel, wildlifeText);
            wildlifeSlide.Display();
            GenericMenu mayorSlide = new(GameArt.Village, mayorText);
            mayorSlide.Display();
            Console.Clear();

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
            "- perdita\n\n" +
            "This game features ASCII art, partially or wholly created by:\n" +
            "Joan Stark (jgs) - mountain backdrop for village elder's house, ocean sunset\n" +
            "Ric_Hotchkiss_sdrc_com - village elder's house\n" +
            "Steven Maddison - left side of village\n" +
            "dgb/itz - docks sunset and background\n" +
            "gnv - coast graphic";

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
            Console.WriteLine("\nPress any key to continue.");
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
}
