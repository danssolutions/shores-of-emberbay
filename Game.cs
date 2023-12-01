namespace TownOfZuul
{
    public class Game
    {
        private bool continuePlaying = true;

        // All locations in the game
        private readonly Village village = new();
        private readonly ElderHouse elderHouse = new();
        private readonly Docks docks = new();
        private readonly Ocean ocean = new();
        private readonly ResearchVessel researchVessel;
        private readonly Coast coast;
        private readonly WastePlant wastePlant;
        
        private Location? currentLocation;
        private readonly Stack<Location> previousLocations = new();
        private readonly List<FishableLocation> fishableLocations = new();
        private readonly List<CleanableLocation> cleanableLocations = new();
        private uint monthCounter;
        private const uint endingMonth = 13;
        public bool ReportsUnlocked = false;
        public Game()
        {
            researchVessel = new(500.0);
            coast = new(500.0);
            wastePlant = new(500.0);

            SetLocationExits();

            fishableLocations.AddRange(new List<FishableLocation>() { docks, ocean });
            cleanableLocations.AddRange(new List<CleanableLocation>() { coast, researchVessel, wastePlant });

            currentLocation = village;
            monthCounter = 1;
        }

        private void SetLocationExits()
        {
            village.SetExits(null, docks, coast, elderHouse); // North, East, South, West
            docks.SetExits(researchVessel, ocean, null, village);
            elderHouse.SetExit("east", village);
            researchVessel.SetExit("south", docks);
            ocean.SetExit("west", docks);
            coast.SetExits(village, null, wastePlant, null);
            wastePlant.SetExit("north", coast);
        }

        public void Play()
        {
            Parser parser = new();

            Console.WriteLine(currentLocation?.Art);
            PrintWelcome();

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
                        if (currentLocation?.Character is not null)
                        {
                            currentLocation?.Character?.Display();
                            Console.Clear();
                            Console.WriteLine(currentLocation?.Art);
                        }
                        else
                            currentLocation?.DefaultNoCharacters();
                        break;

                    case "info":
                        currentLocation?.GetLocationInfo();
                        // researchVessel is a special case since it also shows additional info sourced from other locations.
                        if (currentLocation?.Name == "Research Vessel")
                            ResearchVessel.ShowResearchStats(fishableLocations, cleanableLocations, GetWaterQualityPercentage());
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

                        if (uint.TryParse(command.SecondWord, out uint assignedVillagers))
                            village.SetFreeVillagers((currentLocation?.AssignVillagers(assignedVillagers, village.FreeVillagers)).GetValueOrDefault());
                        else
                            Console.WriteLine("\"" + command.SecondWord + "\" is not a valid or accepted number. Please try again.");
                        break;

                    case "unassign":
                        village.SetFreeVillagers((currentLocation?.AssignVillagers(0, village.FreeVillagers)).GetValueOrDefault());
                        break;

                    case "boo":
                        Console.WriteLine(" .-.\n(o o) boo!\n| O \\\n \\   \\\n  `~~~'\n");
                        break;

                    case "xmas":
                        Console.WriteLine("             *\r\n            /.\\\r\n           /..'\\\r\n           /'.'\\\r\n          /.''.'\\\r\n          /.'.'.\\\r\n         /'.''.'.\\\n         ^^^[_]^^^\r\n\r\n");
                        break;
                    
                    case "sleep":
                        UpdateGame();
                        break;

                    case "report":
                        //UnlockReports();
                        if (ReportsUnlocked)
                            GetReport();
                        else
                            Console.WriteLine("There are no reports available right now.");
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
                // Special check if player attempts to enter ocean - do they have enough villagers to unlock it?
                if (currentLocation?.Name == "Docks" && direction == "east")
                {
                    Docks docks = (Docks)currentLocation;
                    if (!docks.IsOceanUnlocked(village.PopulationCount))
                    {
                        Console.Clear();
                        Console.WriteLine(currentLocation?.Art);
                        Console.WriteLine("Unfortunately, there are no seaworthy vessels that can take you to the ocean right now. \nPerhaps one will be available when the village grows larger?");
                        return;
                    }
                }

                if (currentLocation != null) previousLocations.Push(currentLocation);
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

        private void UnlockReports()
        {
            ReportsUnlocked = true;
            string reportsText = "Emberbay has reached a population of 400! \n" +
            "Well done - your only remaining task is to maintain the population count and keep everyone healthy for the rest of the year.\n" +
            "Upon hearing the news, your superiors have decided to appoint a secretary whose job is to collect all information from the village into monthly reports.\n" +
            "You can now view these reports at any time by using the \"report\" command in any location.";
            GenericMenu reportsEvent = new(GameArt.Village, reportsText);
            reportsEvent.Display();
        }

        private void GetReport()
        {
            Console.WriteLine($"\n- Report for month #{monthCounter} -\n");

            village.GetLocationInfo();
            ResearchVessel.ShowResearchStats(fishableLocations, cleanableLocations, GetWaterQualityPercentage());

            foreach (FishableLocation fishableLocation in fishableLocations)
                fishableLocation.GetLocationInfo();
            Console.WriteLine();
            foreach (CleanableLocation cleanableLocation in cleanableLocations)
                cleanableLocation.GetLocationInfo();
            Console.WriteLine();
        }
        public static void AdvanceMonth(uint monthCounter)
        {
            string advanceText = "You wrap up the plans for this month and note them down. Tomorrow they will be put into action.\n\n" +
            "Time passes, and eventually, month #" + monthCounter + " arrives.\n" +
            "As you prepare for planning once again, you wonder how the village has kept itself up " +
            "since you last examined it and are eager to find out.\n";

            GenericMenu advancementMenu = new(GameArt.AdvanceMonth, advanceText);
            advancementMenu.Display();
        }

        public void UpdateGame()
        {
            monthCounter++;
            if (monthCounter != endingMonth)
                AdvanceMonth(monthCounter);

            foreach (FishableLocation fishableLocation in fishableLocations)
                village.AddToFoodStock(fishableLocation.CatchFish());

            foreach (CleanableLocation cleanableLocation in cleanableLocations)
                cleanableLocation.CleanPollution();

            // Update actual fish reproduction rates based on water quality and population (and base repop rate, and biodiversity score)
            foreach (FishableLocation fishableLocation in fishableLocations)
                fishableLocation.UpdateFishPopulation(GetWaterQualityPercentage());

            village.UpdatePopulation(GetWaterQualityPercentage());

            if (village.PopulationCount < 3)
            {
                Ending.ShowGameOverSlides();
                continuePlaying = false;
            }

            if (monthCounter == endingMonth)
            {
                Ending ending = new();
                continuePlaying = Ending.GetEnding(village.PopulationCount, village.PopulationHealth);
            }

            if (village.PopulationCount >= 400 && ReportsUnlocked == false)
                UnlockReports();

            Console.Clear();
            Console.WriteLine(currentLocation?.Art);
        }

        private double GetWaterQualityPercentage()
        {
            double waterPollution = 0;
            foreach (CleanableLocation cleanableLocation in cleanableLocations)
                waterPollution += 0.25 * (cleanableLocation.PollutionCount / cleanableLocation.InitialPollution);
            double waterQuality = 1.0 - waterPollution;
            return waterQuality;
        }
        private static void PrintWelcome()
        {
            Console.WriteLine("Welcome to the village of Emberbay!");
            Console.WriteLine("Type 'help' for a list of commands.");
            //PrintHelp();
            Console.WriteLine();
        }
        private static void PrintHelp()
        {
            Console.WriteLine("You are the mayor of the village of Emberbay.");
            Console.WriteLine("Your job is to manage the village's population and assign villagers from your settlement to do certain tasks, such as fishing.");
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details about your current location.");
            Console.WriteLine("Type 'back' to go to the previous location.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' or 'exit' to exit to main menu.");
            Console.WriteLine("Type 'talk' to have an interaction with any locals.");
            Console.WriteLine("Type 'info' to get more information from your current location.");
            Console.WriteLine("Type 'assign [number]' to assign a specified amount of villagers to your current location (if possible).");
            Console.WriteLine("Type 'sleep' to advance to the next month.");
            Console.WriteLine("Press Ctrl + C to immediately close this application.");
        }
    }
}