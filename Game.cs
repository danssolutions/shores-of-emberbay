namespace TownOfZuul
{
    public class Game
    {
        private Location? currentLocation;
        private readonly Stack<Location> previousLocations = new();

        public Game()
        {
            CreateLocations();
        }

        private void CreateLocations()
        {
            Village? village = new();
            ElderHouse? elderHouse = new();
            Docks? docks = new();
            ResearchVessel? researchVessel = new();
            Ocean? ocean = new();
            Coast? coast = new();
            WastePlant? wastePlant = new();

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
                        }
                        break;

                    case "north":
                    case "south":
                    case "east":
                    case "west":
                        Move(command.Name);
                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;

                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }

            Console.WriteLine("Thank you for playing Town Of Zuul!");
        }

        private void Move(string direction)
        {
            if (currentLocation?.Exits.ContainsKey(direction) == true)
            {
                previousLocations.Push(currentLocation);
                currentLocation = currentLocation?.Exits[direction];
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
        }


        private static void PrintWelcome()
        {
            Console.WriteLine("Welcome to the Town Of Zuul!");
            Console.WriteLine("Town Of Zuul is a new, incredibly boring adventure game.");
            PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("You are lost. You are alone. You wander");
            Console.WriteLine("around the island.");
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous location.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
        }
    }
}
