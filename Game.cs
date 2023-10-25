namespace TownOfZuul
{
    public class Game
    {
        private Location? currentLocation;
        private Location? previousLocation;

        public Game()
        {
            CreateLocations();
        }

        private void CreateLocations()
        {
            Location? village = new("Village", "You're in the village.");
            Location? villageElderHouse = new("Village Elder's house", "You're in the village elder's house.");
            Location? docks = new("Docks", "You're in the docks.");
            Location? researchVessel = new("Research Vessel", "You're in the research vessel.");
            Location? ocean = new("Ocean", "You're in the ocean.");
            Location? coast = new("Coast", "You're in the coast.");
            Location? wastePlant = new("Wastewater Treatment Plant", "You're in the wastewater treatment plant.");

            village.SetExits(null, docks, coast, villageElderHouse); // North, East, South, West

            docks.SetExits(researchVessel, ocean, null, village);

            villageElderHouse.SetExit("east", village);

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
                Console.WriteLine(currentLocation?.ShortDescription);
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
                        Console.WriteLine(currentLocation?.LongDescription);
                        break;

                    case "back":
                        if (previousLocation == null)
                            Console.WriteLine("You can't go back from here!");
                        else
                            currentLocation = previousLocation;
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
                previousLocation = currentLocation;
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
