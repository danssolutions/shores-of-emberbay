using System.Reflection;

namespace TownOfZuul
{
    public class Game
    {
        private bool continuePlaying = true;
        private Location? currentLocation;
        private readonly Stack<Location> previousLocations = new();
        private readonly List<FishableLocation> fishableLocations = new();
        private readonly List<CleanableLocation> cleanableLocations = new();

        // Note: this might be moved back to CreateLocations() at some point
        

        uint monthCounter;
        const uint endingMonth = 13;
        //int initialPopulation;
        //double populationHealth;
        //int foodStock;
        public bool AlgaeCleanerUnlocked = false;

        public Game()
        {
            CreateLocations();
            //UpdateGame();
            monthCounter = 1;
            //initialPopulation = 300;
            //populationHealth = 30.0;
            //foodStock = 10000;
        }

        private void CreateLocations()
        {
            Village? village;
            ElderHouse? elderHouse;
            Docks? docks;
            Ocean? ocean;
            ResearchVessel? researchVessel;
            Coast? coast;
            WastePlant? wastePlant;
            
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

            fishableLocations.AddRange(new List<FishableLocation>() { docks, ocean });
            cleanableLocations.AddRange(new List<CleanableLocation>() { coast, researchVessel, wastePlant });

            currentLocation = village;
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
                        UpdateGame();
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

            /*Console.WriteLine("Population count: " + village?.PopulationCount);
            if (village != null)
                Console.WriteLine("Population health: " + Math.Round(village.PopulationHealth * 100, 2) + "%");
            Console.WriteLine("Village food stock: " + village?.FoodUnits);
            Console.WriteLine();

            Console.WriteLine($"Ocean unlocked: " + (docks != null && docks.OceanUnlocked ? "Yes" : "No"));
            Console.WriteLine($"Algae cleaner obtained: " + (researchVessel != null && researchVessel.CleanupUnlocked ? "Yes" : "No"));
            Console.WriteLine($"Membrane filter obtained: " + (wastePlant != null && wastePlant.CleanupUnlocked ? "Yes" : "No"));
            Console.WriteLine();

            Console.WriteLine("Macroplastic initial pollution: " + coast?.InitialPollution);
            Console.WriteLine("Nutrient initial pollution: " + researchVessel?.InitialPollution);
            Console.WriteLine("Microplastic initial pollution: " + wastePlant?.InitialPollution);
            Console.WriteLine("Macroplastic pollution: " + coast?.PollutionCount);
            Console.WriteLine("Nutrient pollution: " + researchVessel?.PollutionCount);
            Console.WriteLine("Microplastic pollution: " + wastePlant?.PollutionCount);
            Console.WriteLine("Water quality: " + Math.Round(GetWaterQualityPercentage() * 100, 2) + "% pure");
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
            Console.WriteLine();*/
        }

        public static void AdvanceMonth(uint monthCounter)
        {
            if (monthCounter != endingMonth)
            {
                // TODO: replace AdvanceMonth() art
                string advanceArt = 
            @"


            Nap time, sleeby eeby
        I am placeholder art, replace me!
            
                                                                     
---------------------------------------------------------
                                                                     
                ";

                string advanceText = "You wrap up the plans for this month and note them down. Tomorrow they will be put into action.\n\n" +
                "Time passes, and eventually, month #" + monthCounter + " arrives.\n" + 
                "As you prepare for planning once again, you wonder how the village has kept itself up " +
                "since you last examined it and are eager to find out.\n";

                GenericMenu advancementMenu = new(advanceArt, advanceText);
                advancementMenu.Display();
            }
        }
        
        public void UpdateGame()
        {
            AdvanceMonth(monthCounter);

            foreach (FishableLocation fishableLocation in fishableLocations)
                fishableLocation.CatchFish();
            
            foreach (CleanableLocation cleanableLocation in cleanableLocations)
                cleanableLocation.CleanPollution();
            
            // Update actual fish reproduction rates based on water quality and population (and base repop rate, and biodiversity score)
            foreach (FishableLocation fishableLocation in fishableLocations)
                fishableLocation.UpdateFishPopulation(GetWaterQualityPercentage());
            
            /*if (village != null)
            {
                // Population count is updated based on food stock, and existing health
                uint newVillagers = (uint)((village.FoodUnits - village.PopulationCount) * village.PopulationHealth * 0.8);
                if (newVillagers > 150)
                    newVillagers = 150;
                village.AddPopulation(newVillagers);
                double leftovers = village.ConsumeFoodStock(village.PopulationCount);
                // Population health is updated dependent on food, water quality
                if (leftovers < 0)
                {
                    village.SetPopulationHealth(1.0 - (leftovers / village.PopulationCount));
                }
                else
                {
                    village.SetPopulationHealth(1.5); //improves by 50% if all food needs are met
                }
                // Health naturally decreases when water quality < 30%, and improves (slowly) when water quality goes up
                village.SetPopulationHealth(1.0 + 0.1 * (GetWaterQualityPercentage() - 0.3));
            }*/
            
            monthCounter++;

            // Check ending here
            if (monthCounter == endingMonth)
            {
                Ending ending = new();
                ending.ShowGoodEnding();
                EndingMenu endingMenu = new();
                endingMenu.Display();
                if (endingMenu.StopGame)
                {
                    continuePlaying = false;
                    return;
                }
            }
            
            Console.Clear();
            Console.WriteLine(currentLocation?.Art);
        }

        private double GetWaterQualityPercentage()
        {
            double waterPollution = 0;
            foreach (CleanableLocation cleanableLocation in cleanableLocations)
                waterPollution += cleanableLocation.PollutionCount / cleanableLocation.InitialPollution;
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
            Console.WriteLine("Type 'sleep' to advance to the next month.");
            Console.WriteLine("Type 'close' to immediately close this application.");
        }
    }
}