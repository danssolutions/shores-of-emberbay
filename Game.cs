using System.Reflection;

namespace TownOfZuul
{
    public class Game
    {
        private Location? currentLocation;
        private readonly Stack<Location> previousLocations = new();

        // Note: this might be moved back to CreateLocations() at some point
        Village? village;
        ElderHouse? elderHouse;
        Docks? docks;
        Ocean? ocean;
        ResearchVessel? researchVessel;
        Coast? coast;
        WastePlant? wastePlant;

        uint monthCounter;
        //int initialPopulation;
        //double populationHealth;
        //int foodStock;
        public bool AlgaeCleanerUnlocked = false;

        public Game()
        {
            CreateLocations();
            //AdvanceMonth();
            monthCounter = 1;
            //initialPopulation = 300;
            //populationHealth = 30.0;
            //foodStock = 10000;
        }

        private void CreateLocations()
        {
            village = new();
            elderHouse = new();
            docks = new();
            ocean = new();
            researchVessel = new(500.0);
            coast = new(500.0);
            wastePlant = new(500.0);

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

                switch (command.Name)
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

                    case "sleep":
                        AdvanceMonth();
                        break;
                    
                    case "close":
                        CloseGame();
                        break;
                    
                    case "report":
                        GetReport();
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
                Console.Clear();
                Console.WriteLine(currentLocation?.Art);
                Console.WriteLine($"There's nothing of interest towards the {direction}.");
            }
        }

        private void GetReport()
        {
            // TODO: split all these into separate methods, since they'd be useful outside this function also (and probably help w/ encapsulation)
            Console.WriteLine("\n- Report -");

            Console.WriteLine("Population count: " + village?.PopulationCount);
            Console.WriteLine("Population health: " + village?.PopulationHealth);
            Console.WriteLine();

            Console.WriteLine($"Ocean unlocked: " + (docks != null && docks.OceanUnlocked ? "Yes" : "No"));
            Console.WriteLine($"Algae cleaner obtained: " + (researchVessel != null && researchVessel.CleanupUnlocked ? "Yes" : "No"));
            Console.WriteLine($"Membrane filter obtained: " + (wastePlant != null && wastePlant.CleanupUnlocked ? "Yes" : "No"));
            Console.WriteLine();

            Console.WriteLine("Macroplastic pollution: " + coast?.PollutionCount);
            Console.WriteLine("Nutrient pollution: " + coast?.PollutionCount);
            Console.WriteLine("Microplastic pollution: " + coast?.PollutionCount);
            Console.WriteLine();

            Console.WriteLine("Total fish in the docks: " + docks?.LocalFish.Sum(item => item.Population));
            for (int i = 0; i < docks?.LocalFish.Count; i++)
                Console.WriteLine("- " + docks?.LocalFish[i].Name + ": " + docks?.LocalFish[i].Population + " (previously " + docks?.LocalFish[i].PreviousPopulation + ")");
            Console.WriteLine("Total fish in the ocean: " + ocean?.LocalFish.Sum(item => item.Population));
            for (int i = 0; i < ocean?.LocalFish.Count; i++)
                Console.WriteLine("- " + ocean?.LocalFish[i].Name + ": " + ocean?.LocalFish[i].Population + " (previously " + ocean?.LocalFish[i].PreviousPopulation + ")");
            Console.WriteLine();
            // TODO: add reproduction rates to each fish also

            Console.WriteLine("Villagers fishing in docks: " + (docks?.LocalFishers.Sum(item => Convert.ToUInt32(item))));
            for (int i = 0; i < docks?.LocalFishers.Count; i++)
                Console.WriteLine("- " + docks?.LocalFish[i].Name + " fishers: " + docks?.LocalFishers[i]);
            Console.WriteLine("Villagers fishing in the ocean: " + (ocean?.LocalFishers.Sum(item => Convert.ToUInt32(item))));
            for (int i = 0; i < ocean?.LocalFishers.Count; i++)
                Console.WriteLine("- " + ocean?.LocalFish[i].Name + " fishers: " + ocean?.LocalFishers[i]);
            Console.WriteLine("Villagers cleaning the coast: " + coast?.LocalCleaners);
            Console.WriteLine("Villagers helping with algae cleanup in the research vessel: " + researchVessel?.LocalCleaners);
            Console.WriteLine("Villagers operating the filter in the wastewater plant: " + wastePlant?.LocalCleaners);
            Console.WriteLine();
        }

        /*
        Typical game cycle:
        Player goes to various places, allocates villagers to do certain stuff
        Player goes to next day/month
        Villagers do their thing (either catch fish, and/or clean stuff)
        First, water quality is updated
        Fish reproduction rates are tweaked based on water quality
        Fish stocks are tweaked dependent on amount fished (or amount of villagers fishing), as well as reproduction rates and water quality.
        This info is captured by the research team and passed to the player in the next cycle.
        Food stock is updated dependent on amount fished
        Population health is updated dependent on food, water quality
        Population count is updated dependent on population health
        Day/month incremented by 1
*/
        public void AdvanceMonth()
        {
            uint catchAmount;

            Random random = new();

            // 1 villager can fish 30-200 fish per month
            // 1 villager eats ~90 fish per month (aka 1 food unit)
            // 1 villager gets 300 attempts a month

            for (int fishType = 0; fishType < docks?.LocalFish.Count; fishType++) // for each type of fish in docks
            {
                docks?.LocalFish[fishType].SetPreviousPopulation();
                uint totalCatchAmount = 0;
                for (uint i = 0; i < docks?.LocalFishers[fishType]; i++) // for each fisher catching a specific fish type
                {
                    catchAmount = (uint)(random.Next(30, 200) * (1.0 - (double)docks.LocalFish[fishType].CatchDifficulty.GetValueOrDefault()));

                    if (catchAmount > docks.LocalFish[fishType].Population)
                        catchAmount = docks.LocalFish[fishType].Population;

                    docks?.LocalFish[fishType].SetPopulation(catchAmount);
                    
                    // TODO: try for bycatch here
                    // try for bycatch: iterate through random fish in this location and attempt to catch any one of them

                    totalCatchAmount += catchAmount;
                    Console.WriteLine("Villager #" + (i + 1) + " caught " + catchAmount + " " + docks?.LocalFish[fishType].Name + " this month.");
                }
                //docks?.LocalFish[fishType].SetPopulation(totalCatchAmount);
            }
            /*for (int fishType = 0; fishType < ocean?.LocalFish.Count; fishType++)
            {
                uint totalCatchAmount = 0;
                for (uint i = 1; i <= ocean?.LocalFishers[fishType]; i++)
                {
                    catchAmount = (uint)(random.Next(30, 200) * (double)ocean.LocalFish[fishType].CatchDifficulty.GetValueOrDefault());

                    if (catchAmount > ocean.LocalFish[fishType].Population)
                        catchAmount = ocean.LocalFish[fishType].Population;

                    
                    // TODO: try for bycatch here

                    totalCatchAmount += catchAmount;
                    Console.WriteLine("Villager #" + i + " caught " + catchAmount + " " + ocean?.LocalFish[fishType].Name + " this month.");
                }
                ocean?.LocalFish[fishType].SetPopulation(totalCatchAmount);
            }*/

            // Village cleaner stuff here

            // Update water quality here (whichever vars happen to represent it)

            // Update actual fish reproduction rates based on water quality and population (and base repop rate)

            // Fish stocks are tweaked dependent on amount fished (or amount of villagers fishing), as well as reproduction rates.

            // Food stock is updated dependent on amount fished

            // Population health is updated dependent on food, water quality

            // Population count is updated dependent on population health

            // Day/month incremented by 1

            monthCounter++;

            // Check ending here

            AdvancementMenu advancementMenu = new(monthCounter);
            advancementMenu.Display();

            Console.Clear();
            Console.WriteLine(currentLocation?.Art);

            // village?.PopulationCount;
            // village?.PopulationHealth;
            // docks?.LocalFishers[];
            // ocean?.LocalFishers[];
            // coast.LocalCleaners;
            // researchVessel.LocalCleaners;
            // wastePlant.LocalCleaners;

            /*monthCounter++;
            int populationChange = 0;
            //for now I made everything random just trying to make stuff I will improve the system as we go on.
            //if you feel like this way is not to good let me know how to improve it :)
            if (monthCounter <= 12)
            {
                Random random = new Random();

                for (int day = 1; day <= 30; day++)
                {
                    if (populationHealth > 90) { populationChange = random.Next(0, 5); }
                    else if (populationHealth <= 90 && populationHealth > 70) { populationChange = random.Next(-2, 4); }
                    else if (populationHealth <= 70 && populationHealth > 50) { populationChange = random.Next(-4, 2); } // for now its ranndom change in population
                    else if (populationHealth <= 50 && populationHealth >= 0) { populationChange = random.Next(-5, 0); }

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
            else
            {
                //Final ending after 12 months
            }*/
        }
        static void CloseGame()
        {
            Console.Clear();
            Console.WriteLine(Program.QuitMessage);
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
        }
    }
}