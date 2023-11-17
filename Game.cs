namespace TownOfZuul
{
    public class Game
    {
        private Location? currentLocation;
        private Menu? menus;
        //private Location? characterLocation;
        private readonly Stack<Location> previousLocations = new();
        int monthCounter = 1;
        int initialPopulation;
        double populationHealth;
        int foodStock;
        public bool AlgaeCleanerUnlocked = false;

        public Game()
        {
            CreateLocations();
            AdvanceMonth();
            monthCounter = 0;
            initialPopulation = 300;
            populationHealth = 30.0;
            foodStock = 10000;
        }

        private void CreateLocations()
        {
            Village? village = new();
            ElderHouse? elderHouse = new();
            Docks? docks = new();
            Ocean? ocean = new();
            ResearchVessel? researchVessel = new(500.0);
            Coast? coast = new(500.0);
            WastePlant? wastePlant = new(500.0);

            village.SetExits(null, docks, coast, elderHouse); // North, East, South, West

            docks.SetExits(researchVessel, ocean, null, village);

            elderHouse.SetExit("east", village);

            researchVessel.SetExit("south", docks);

            ocean.SetExit("west", docks);

            coast.SetExits(village, null, wastePlant, null);

            wastePlant.SetExit("north", coast);

            currentLocation = village;
        }

        public void Play()
        {
            Parser parser = new();

            Console.WriteLine(currentLocation?.Art);
            PrintWelcome();

            bool continuePlaying = true;
            while (continuePlaying)
            {
                Console.WriteLine(currentLocation?.Name);
                Console.Write("> ");

                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a command.");
                    continue;
                }

                Command? command = parser.GetCommand(input);

                if (command == null)
                {
                    Console.WriteLine("I don't know that command.");
                    continue;
                }

                switch(command.Name)
                {
                    case "look":
                        Console.WriteLine(currentLocation?.Description);
                        break;

                    case "back":
                        if (previousLocations.Count == 0)
                            Console.WriteLine("You can't go back from here!");
                        else
                        {
                            currentLocation = previousLocations.First();
                            previousLocations.Pop();
                            Console.Clear();
                            Console.WriteLine(currentLocation?.Art);
                        }
                        break;

                    case "north":
                    case "south":
                    case "east":
                    case "west":
                        Console.Clear();
                        Move(command.Name);
                        break;

                    case "exit":
                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;

                    case "talk":
                        Console.WriteLine(currentLocation?.Dialogue);
                        break;

                    //case "talk":
                    //    Console.WriteLine(Character?.Dialogue);
                    //    break;

                    case "story":
                        Console.WriteLine(currentLocation?.Story);
                        break;

                    case "info":
                        Console.WriteLine(currentLocation?.Information);
                        break;
                    
                    case "clear":
                        Console.Clear();
                        break;
                    
                    case "assign":
                        if (command.SecondWord == null)
                        {
                            Console.WriteLine("The 'assign' command requires defining a number of villagers to be assigned, i.e. \"assign 5\".");
                            break;
                        }

                        if (uint.TryParse(command.SecondWord, out uint result))
                            currentLocation?.AssignVillagers(result);
                        else
                            Console.WriteLine("\"" + command.SecondWord + "\" is not a valid or accepted number. Please try again.");
                        break;
                    
                    case "unassign":
                        currentLocation?.AssignVillagers(0);
                        break;
                    
                    case "boo":
                        Console.WriteLine(" .-.\n(o o) boo!\n| O \\\n \\   \\\n  `~~~'\n");
                        break;

                    case "xmas":
                        Console.WriteLine("             *\r\n            /.\\\r\n           /..'\\\r\n           /'.'\\\r\n          /.''.'\\\r\n          /.'.'.\\\r\n         /'.''.'.\\\n         ^^^[_]^^^\r\n\r\n");
                        break;

                    case "algae":
                        AlgaeCleanerUnlocked = true;
                        Console.WriteLine("Great, you now have the algae cleaner and can start cleaning the algae.");
                        break;

                    case "speak":
                        DialogueMenu dialogueMenu = new();
                        dialogueMenu.Display();
                        break;

                    case "sleep":
                        AdvanceMonth();
                        Console.WriteLine($"Your village advances to the {monthCounter} month. Your population health: {populationHealth}%.");
                        Console.WriteLine($"Your villages population: {initialPopulation}. Food stock:{foodStock}");
                        Console.WriteLine();
                        break;
                    case "close":
                        CloseGame();
                        break;

                    default:
                        Console.WriteLine("I don't know what command...");
                        break;
                }
            }

            Console.Clear();
        }

        private void Move(string direction)
        {
            if (currentLocation?.Exits.ContainsKey(direction) == true)
            {
                previousLocations.Push(currentLocation);
                currentLocation = currentLocation?.Exits[direction];

                Console.Clear();
                Console.WriteLine(currentLocation?.Art);
            }
            else
            {
                Console.WriteLine($"There's nothing of interest towards the {direction}.");
            }
        }

        public void AdvanceMonth()
        {
            monthCounter++;

            //for now I made everything random just trying to make stuff I will improve the system as we go on.
            //if you feel like this way is not to good let me know how to improve it :)

            Random random = new Random();

            for (int monthCounter = 1; monthCounter <= 12; monthCounter++)
            {
                int populationChange = random.Next(-10, 11); // for now its ranndom change in population

                initialPopulation += populationChange;
                if (initialPopulation < 0)
                {
                    initialPopulation = 0;
                }

                double healthChange = random.Next(-5, 6); //for now its random change in population health

                populationHealth += healthChange;
                if (populationHealth < 0)
                {
                    populationHealth = 0;
                }
                else if (populationHealth > 100)
                {
                    populationHealth = 100;
                }

                int foodConsumption = initialPopulation * 30;

                int minValue = 5000;
                int maxValue = 10000;

                int foodStockChange = random.Next(minValue, maxValue);

                foodStock += foodStockChange;

                if (foodStock >= foodConsumption)
                {
                    foodStock -= foodConsumption;
                }
                else
                {
                    foodStock -= foodConsumption;
                }
            }
        }
         static void CloseGame()
        {
            Console.Clear();
            Console.WriteLine(MainMenu.QuitMessage);
            Environment.Exit(0);
        }


        private static void PrintWelcome()
        {
            Console.WriteLine("Welcome to Town Of Zuul!");
            Console.WriteLine("Type 'help' for a list of commands.");
            //PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("You are a mayor of the town of Zuul.");
            Console.WriteLine("Your job is to manage the town's population and assign villagers from your town to do certain tasks, such as fishing.");
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details about your current location.");
            Console.WriteLine("Type 'back' to go to the previous location.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' or 'exit' to exit the game.");
            Console.WriteLine("Type 'talk' to have an interaction with any locals.");
            Console.WriteLine("Type 'info' to get more information from your current location.");
            Console.WriteLine("Type 'assign [number]' to assign a specified amount of villagers to your current location (if possible).");
            //Console.WriteLine("Type 'sleep' to advance to the next month.");
            Console.WriteLine("Type 'close' to immediately close this application.");
            Console.WriteLine("Type 'speak' to open the speak menu.");
        }

        private static void PrintSpeak()
        {
            /*Console.WriteLine("");
            Console.WriteLine("Speak menu is used to communicate withe the characters");
            Console.WriteLine("");
            Console.WriteLine("Type (more) to Keep talking with the character");
            Console.WriteLine("Type (stop) if you have heard enough, and wish to move on");
            Console.WriteLine("Type (who) to learn more about how the character can help");
            Console.WriteLine("Type (items) to ask what items you can unlock and how");
            Console.WriteLine("Type (unlock) to unlock item");
            */
        }
    }
}