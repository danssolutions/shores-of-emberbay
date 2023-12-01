using System.Reflection;
using System.Security.Permissions;

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

        public int PopulationCount { get; private set; }
        public int FreeVillagers { get; private set; }
        public double PopulationHealth { get; private set; }
        public double FoodUnits { get; private set; }
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
            
            PopulationCount = FreeVillagers = 10;
            PopulationHealth = 0.5;
            FoodUnits = 15.0;
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
                        currentLocation?.Character?.Display();
                        Console.Clear();
                        Console.WriteLine(currentLocation?.Art);
                        break;

                    case "info":
                        currentLocation?.GetLocationInfo();
                        // researchVessel is a special case since it also shows info about other fishableLocations
                        if (currentLocation?.Name == "Research Vessel")
                        {
                            Console.WriteLine();
                            ShowWaterQuality();
                            ResearchVessel.ShowFishStats(fishableLocations);
                        }
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
                            FreeVillagers = (currentLocation?.AssignVillagers(assignedVillagers, FreeVillagers)).GetValueOrDefault();
                        else
                            Console.WriteLine("\"" + command.SecondWord + "\" is not a valid or accepted number. Please try again.");
                        break;

                    case "unassign":
                        FreeVillagers = (currentLocation?.AssignVillagers(0, FreeVillagers)).GetValueOrDefault();
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

                    case "close":
                        CloseGame();
                        break;

                    case "report":
                        UnlockReports();
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
                    if (!docks.IsOceanUnlocked(PopulationCount))
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

        // TODO remove this
        private void ShowPopulationStats()
        {
            Console.WriteLine("Population count: " + PopulationCount);
            Console.WriteLine("Villagers free for assignment: " + FreeVillagers);
            Console.WriteLine("Population health: " + Math.Round(PopulationHealth * 100, 2) + "%");
            Console.WriteLine("Current food stock: " + FoodUnits + " monthly ration" + (FoodUnits == 1 ? "" : "s"));
        }

        private void ShowWaterQuality()
        {
            Console.WriteLine("Water quality: " + Math.Round(GetWaterQualityPercentage() * 100, 2) + "% pure");
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

            ShowPopulationStats();
            Console.WriteLine();

            ShowWaterQuality();
            ResearchVessel.ShowFishStats(fishableLocations);

            foreach (FishableLocation fishableLocation in fishableLocations)
                fishableLocation.GetLocationInfo();
            Console.WriteLine();
            foreach (CleanableLocation cleanableLocation in cleanableLocations)
                cleanableLocation.GetLocationInfo();
            Console.WriteLine();
        }

        public void AddToFoodStock(double? additionalFood)
        {
            FoodUnits += additionalFood.GetValueOrDefault();
        }
        public double ConsumeFoodStock(double? foodAmount)
        {
            FoodUnits -= foodAmount.GetValueOrDefault();
            double leftovers = FoodUnits;
            if (FoodUnits < 0)
                FoodUnits = 0;
            return leftovers;
        }

        public void UpdatePopulation()
        {
            double leftovers = ConsumeFoodStock(PopulationCount);
            // Population health is updated dependent on food, water quality
            if (leftovers < 0)
            {
                SetPopulationHealth(1.0+(leftovers/PopulationCount));
            }
            else
            {
                SetPopulationHealth(1.5); //improves by 50% if all food needs are met
            }
            // Health naturally decreases when water quality < 30%, and improves (slowly) when water quality goes up
            SetPopulationHealth(0.7 + 0.5*(GetWaterQualityPercentage() - 0.3));
            // Population count is updated based on food stock, and existing health
            int newVillagers = (int)(leftovers * (leftovers >= 0 ? PopulationHealth : 1.0));
            if (newVillagers > 50)
                newVillagers = 50;
            PopulationCount += newVillagers;
            if(PopulationCount<=0)
            {
                PopulationCount=0;
                SetPopulationHealth(0);
            }
        }

        public void SetPopulationHealth(double multiplier)
        {
            PopulationHealth *= multiplier;
            if (PopulationHealth <= 0.0)
                PopulationHealth = 0.0;
            else if (PopulationHealth > 1.0)
                PopulationHealth = 1.0;
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
                AddToFoodStock(fishableLocation.CatchFish());

            foreach (CleanableLocation cleanableLocation in cleanableLocations)
                cleanableLocation.CleanPollution();

            // Update actual fish reproduction rates based on water quality and population (and base repop rate, and biodiversity score)
            foreach (FishableLocation fishableLocation in fishableLocations)
                fishableLocation.UpdateFishPopulation(GetWaterQualityPercentage());

            UpdatePopulation();

            if (PopulationCount < 3)
            {
                Ending.ShowGameOverSlides();
                continuePlaying = false;
            }

            if (monthCounter == endingMonth)
            {
                Ending ending = new();
                continuePlaying = Ending.GetEnding(PopulationCount, PopulationHealth);
            }

            if (PopulationCount >= 400 && ReportsUnlocked == false)
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

        static void CloseGame()
        {
            Console.Clear();
            Console.WriteLine(Program.QuitMessage);
            Environment.Exit(0);
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
            Console.WriteLine("Type 'quit' or 'exit' to exit the game.");
            Console.WriteLine("Type 'talk' to have an interaction with any locals.");
            Console.WriteLine("Type 'info' to get more information from your current location.");
            Console.WriteLine("Type 'assign [number]' to assign a specified amount of villagers to your current location (if possible).");
            Console.WriteLine("Type 'sleep' to advance to the next month.");
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