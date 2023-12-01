namespace TownOfZuul
{
    public abstract class Location
    {
        public string? Art { get; protected set; }
        public string? Name { get; protected set; }
        public string? Description { get; protected set; }
        public Character? Character { get; protected set; }
        public Dictionary<string, Location> Exits { get; private set; } = new();

        public void SetExits(Location? north, Location? east, Location? south, Location? west)
        {
            SetExit("north", north);
            SetExit("east", east);
            SetExit("south", south);
            SetExit("west", west);
        }

        public void SetExit(string direction, Location? neighbor)
        {
            if (neighbor != null)
                Exits[direction] = neighbor;
        }

        public virtual uint AssignVillagers(uint amount, uint freeVillagers)
        {
            Console.WriteLine("This location cannot have any villagers assigned to it.");
            return freeVillagers;
        }

        public virtual void GetLocationInfo()
        {
            Console.WriteLine("This location does not contain any useful information.");
        }

        public virtual void DefaultNoCharacters()
        {
            Console.WriteLine("There doesn't seem to be anyone to talk to here.");
        }
    }

    public abstract class FishableLocation : Location
    {
        public List<Fish> LocalFish { get; private set; } = new();
        public List<uint> LocalFishers { get; protected set; } = new();

        public override uint AssignVillagers(uint amount, uint freeVillagers)
        {
            uint assignableVillagers = 0;

            for (int i = 0; i < LocalFish.Count; i++)
                assignableVillagers += LocalFishers[i];

            uint totalVillagers = assignableVillagers + freeVillagers;

            if (amount == 0)
            {
                Console.WriteLine("Clearing this location of all fishers...");
                for (int i = 0; i < LocalFish.Count; i++)
                    LocalFishers[i] = 0;

                return totalVillagers;
            }

            if (amount > totalVillagers)
            {
                Console.WriteLine("Cannot assign " + amount + " villagers when there are only " + totalVillagers + " available.");
                return freeVillagers;
            }

            FishingMenu fishMenu = new(this, amount);
            fishMenu.Display();

            LocalFishers = fishMenu.GetFisherList(LocalFishers);

            foreach (uint fishers in LocalFishers)
                totalVillagers -= fishers;

            return totalVillagers;
        }

        public double GetBiodiversityScore()
        {
            return 2.0; // TODO: replace with something meaningful
        }

        public double CatchFish()
        {
            uint catchAmount;
            uint bycatchAmount;
            double foodAmount = 0;

            Random random = new();

            for (int fishType = 0; fishType < LocalFish.Count; fishType++) // for each type of fish in ocean
            {
                LocalFish[fishType].SetPreviousPopulation();
                for (uint i = 0; i < LocalFishers[fishType]; i++) // for each fisher catching a specific fish type
                {
                    catchAmount = (uint)(random.Next(30, 100) * (1.0 - LocalFish[fishType].CatchDifficulty.GetValueOrDefault()));

                    if (catchAmount > LocalFish[fishType].Population)
                        catchAmount = LocalFish[fishType].Population;

                    foodAmount += (LocalFish[fishType].FoodValue * catchAmount).GetValueOrDefault();

                    LocalFish[fishType].RemovePopulation(catchAmount);

                    // try for bycatch: iterate through random fish in this location and attempt to catch any one of them
                    foreach (Fish bycatch in LocalFish) // for each type of fish in docks
                    {
                        bycatchAmount = (uint)(random.Next(1, 12) * random.NextDouble() * (1.0 - bycatch.CatchDifficulty.GetValueOrDefault()));

                        if (bycatchAmount > bycatch.Population)
                            bycatchAmount = bycatch.Population;

                        bycatch.RemovePopulation(bycatchAmount);

                        // Oarfish event check
                        if (bycatchAmount > 0 && bycatch.Name == "Giant Oarfish")
                        {
                            string oarfishEventText = "Woah! Someone from the village caught and and hauled back a giant oarfish, an incredibly rare and massive fish!";
                            GenericMenu oarfishEvent = new(GameArt.Fisherman, oarfishEventText);
                            oarfishEvent.Display();
                        }
                        foodAmount += (bycatch.FoodValue * bycatchAmount).GetValueOrDefault();
                    }
                }
            }
            return foodAmount;
        }

        override public void GetLocationInfo()
        {
            Console.WriteLine($"Villagers fishing in {Name}: " + LocalFishers.Sum(item => Convert.ToUInt32(item)));
            for (int i = 0; i < LocalFishers.Count; i++)
                if (LocalFish[i].BycatchOnly == false)
                    Console.WriteLine("- " + LocalFish[i].Name + " fishers: " + LocalFishers[i]);
        }

        public void UpdateFishPopulation(double waterQuality)
        {
            foreach (Fish fishType in LocalFish)
            {
                fishType.SetReproductionRates(waterQuality);
                // Fish stocks are tweaked dependent on reproduction rates.
                fishType.AddPopulation();
            }
        }

        protected virtual void Populate()
        {
            // sort LocalFish array to make sure fish with bycatchOnly == true are at the end, since otherwise FishingMenu options could bug out
            for (int i = 0; i < LocalFish.Count - 1; i++)
            {
                Fish fish = LocalFish[i];
                if (fish.BycatchOnly == true)
                {
                    LocalFish.RemoveAt(i);
                    LocalFish.Add(fish);
                }
            }

            for (int i = 0; i < LocalFish.Count; i++)
                LocalFishers.Add(0);
        }
    }

    public abstract class CleanableLocation : Location
    {
        // The amount of pollution currently in the location.
        // For different locations, this represents different types of pollution, measured in its own type of unit.
        public readonly double InitialPollution;

        public string PollutionType;
        public string PollutionTypeUnit;
        public double PollutionCount { get; protected set; }

        // Whether this location can be cleaned by villagers assigned to it.
        // Some locations require additional technology to be unlocked in order for it to be cleanable.
        public bool CleanupUnlocked { get; protected set; } = true;

        public uint LocalCleaners { get; private set; } = new();

        public CleanableLocation(double pollutionUnits)
        {
            InitialPollution = PollutionCount = pollutionUnits;
            LocalCleaners = 0;
            PollutionType = PollutionTypeUnit = "";
        }

        public override uint AssignVillagers(uint amount, uint freeVillagers)
        {
            if (!CleanupUnlocked)
            {
                Console.WriteLine("Cannot assign anyone here, as the village does not have the tools necessary for cleanup.");
                return freeVillagers;
            }

            uint totalVillagers = LocalCleaners + freeVillagers;

            if (amount == 0)
            {
                Console.WriteLine("Clearing this location of all cleaners...");
                LocalCleaners = 0;

                return totalVillagers;
            }

            if (amount > totalVillagers)
            {
                Console.WriteLine("Cannot assign " + amount + " villagers when there are only " + totalVillagers + " available.");
                return freeVillagers;
            }

            LocalCleaners = amount;

            Console.WriteLine("Assignment confirmed. \nVillagers ready to clean in this location: " + amount + ".");
            return totalVillagers - LocalCleaners;
        }

        override public void GetLocationInfo()
        {
            if (CleanupUnlocked)
                Console.WriteLine($"Villagers cleaning in the {Name}: " + LocalCleaners);
            else
                Console.WriteLine($"Cleaning in the {Name} is unavailable right now.");
        }

        public void CleanPollution()
        {
            Random random = new();
            if (LocalCleaners > 0)
                PollutionCount -= LocalCleaners * double.Clamp(random.NextDouble(), 0.5, 1.0);
            if (PollutionCount < 0)
                PollutionCount = 0;
        }
    }

    // TODO: put classes below in separate files
    public class Village : Location
    {
        public int PopulationCount { get; private set; }
        public uint FreeVillagers { get; private set; }
        public double PopulationHealth { get; private set; }
        public double FoodUnits { get; private set; }
        public Village()
        {
            Art = GameArt.Village;
            Name = "Village";
            Description = "You're in the village." +
            " Once a large and prosperous place, you can easily tell its glory days are in the past." +
            " Most of the buildings, which used to provide the shelter and livelihood to numerous people " +
            "are now desolate and ill-kept." +
            " Somehow, though, you can feel that this village might get another shot at prosperity.";
            PopulationCount = 10;
            FreeVillagers = 10;
            PopulationHealth = 0.5;
            FoodUnits = 15.0;
        }

        override public void GetLocationInfo()
        {
            Console.WriteLine("Population count: " + PopulationCount);
            Console.WriteLine("Villagers free for assignment: " + FreeVillagers);
            Console.WriteLine("Population health: " + Math.Round(PopulationHealth * 100, 2) + "%");
            Console.WriteLine("Current food stock: " + FoodUnits + " monthly ration" + (FoodUnits == 1 ? "" : "s"));
        }

        public void SetFreeVillagers(uint amount)
        {
            FreeVillagers = amount;
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
        public void UpdatePopulation(double waterQuality)
        {
            double leftovers = ConsumeFoodStock(PopulationCount);
            // Population health is updated dependent on food, water quality
            if (leftovers < 0)
                SetPopulationHealth(1.0 + (leftovers / PopulationCount));
            else
                SetPopulationHealth(1.5); //improves by 50% if all food needs are met
            // Health naturally decreases when water quality < 30%, and improves (slowly) when water quality goes up
            SetPopulationHealth(0.7 + 0.5 * (waterQuality - 0.3));
            // Population count is updated based on food stock, and existing health
            int newVillagers = (int)(leftovers * (leftovers >= 0 ? PopulationHealth : 1.0));
            if (newVillagers > 50)
                newVillagers = 50;
            PopulationCount += newVillagers;
            if (PopulationCount <= 0)
            {
                PopulationCount = 0;
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
        override public void DefaultNoCharacters()
        {
            Console.WriteLine("You try to talk to random villagers, but none of them seem very talkative.");
        }
    }

    public class ElderHouse : Location
    {
        //public Character character = new();
        public bool AlgaeCleanerUnlocked { get; private set; }
        public bool WaterFilterUnlocked { get; private set; }
        private Elder elder = new();

        public ElderHouse()
        {
            Art = GameArt.ElderHouse;
            Name = "Village Elder's house";
            Description = "On the outskirts of town you find yourself looking at a small but well-maintained wooden shack." +
            " Although it is as old as most of the surrounding architecture," +
            " the passage of time has not managed to tear down this testament of the village's past greatness." +
            " You're in front of the village elder's house.\n The elder provides you with knowledge on " +
            "how to take care of the population and expand the village. The village elder will provide what you with "
            + "\n what you need to help the village.";
            AlgaeCleanerUnlocked = false;
            WaterFilterUnlocked = false;
            Character = elder;
        }
    }

    public class Docks : FishableLocation
    {
        public SeaTrout? seaTrout;
        public SeaBass? seaBass;
        public Pike? pike;
        public Salmon? salmon;
        public Sturgeon? sturgeon;
        private Fisherman? fisherman = new();

        public bool OceanUnlocked { get; private set; }

        override protected void Populate()
        {
            Random random = new();

            seaTrout = new((uint)random.Next(500, 1000));
            seaBass = new((uint)random.Next(500, 1000));
            pike = new((uint)random.Next(300, 1000));
            salmon = new((uint)random.Next(300, 1000));
            sturgeon = new((uint)random.Next(200, 1000));

            LocalFish.AddRange(new List<Fish>() { seaTrout, seaBass, pike, salmon, sturgeon });

            base.Populate();
        }
        public Docks()
        {
            Art = GameArt.Docks;
            Name = "Docks";
            Description = "You're at the village docks. " +
            "A place where many of the village people's found employment now lies empty, " +
            "save for the odd boat or seagull. " +
            "A large chunk of the construction has been taken by the sea and the storms throughout the years, " +
            "some of it still floating on the water, rocking with the waves. " +
            "Even still, the view of the waterfront remains as impressive as it has always been.\n\n" +
            "You see a bulletin board containing some kind of *info*, as well as an old fisherman hanging around one of the docks. Someone to *talk* to?";
            OceanUnlocked = false;
            Character = fisherman;

            Populate();
        }

        public bool IsOceanUnlocked(int population = 0)
        {
            if (!OceanUnlocked)
            {
                OceanUnlocked = population > 150;
            }
            return OceanUnlocked;
        }
    }

    public class ResearchVessel : CleanableLocation
    {
        private Scientist scientist = new();
        public ResearchVessel(double pollutionUnits) : base(pollutionUnits)
        {
            Art = GameArt.ResearchVessel;
            Name = "Research Vessel";
            PollutionType = "Phosphorus";
            PollutionTypeUnit = "ug/l";
            Description = "You're in the research vessel. " +
            "You are greeted by the sight of somewhat modern technology and machinery, " +
            "some of which can be considered a rare find nowadays. " +
            "How such equipment has remained so well-maintained to this day is a mystery to you " +
            "but you are nevertheless impressed by its condition. " +
            "If this village and its surroundings are going to be saved, " +
            "you can already tell this ship will be instrumental in achieving that.";

            CleanupUnlocked = false; // cannot clean until nutrient cleaner unlocked
            Character = scientist;
        }

        private static void ShowWaterQuality(double waterQuality)
        {
            Console.WriteLine("Water quality: " + Math.Round(waterQuality * 100, 2) + "% pure");
        }
        public static void ShowResearchStats(List<FishableLocation> fishableLocations, List<CleanableLocation> cleanableLocations, double waterQuality)
        {
            Console.WriteLine();
            foreach (CleanableLocation cleanableLocation in cleanableLocations)
            {
                Console.WriteLine($"{cleanableLocation.PollutionType} pollution: " + cleanableLocation.PollutionCount + " " + cleanableLocation.PollutionTypeUnit);
                //Console.WriteLine($"{cleanableLocation.PollutionType} initial pollution: " + cleanableLocation.InitialPollution + " " + cleanableLocation.PollutionTypeUnit);
            }
            ShowWaterQuality(waterQuality);
            Console.WriteLine();
            foreach (FishableLocation fishableLocation in fishableLocations)
            {
                Console.WriteLine($"Total fish in the {fishableLocation.Name}: " + fishableLocation.LocalFish.Sum(item => item.Population));
                foreach (Fish fish in fishableLocation.LocalFish)
                {
                    if (fish.BycatchOnly == false)
                        Console.WriteLine("- " + fish.Name + ": " + fish.Population + " (previously " + fish.PreviousPopulation + ")");
                }
                Console.WriteLine();
                foreach (Fish fish in fishableLocation.LocalFish)
                {
                    double reproductionRate = fish.ReproductionRate != 0 ? fish.ReproductionRate : fish.BaseReproductionRate;
                    double previousReproductionRate = fish.PreviousReproductionRate != 0 ? fish.PreviousReproductionRate : fish.BaseReproductionRate;
                    if (fish.BycatchOnly == false)
                        Console.WriteLine("- " + fish.Name + " reproduction rate: " + Math.Round(reproductionRate, 2) + " (previously " + Math.Round(previousReproductionRate, 2) + ")");
                }
                Console.WriteLine();
            }
        }
    }

    public class Ocean : FishableLocation
    {
        public Mackerel? mackerel;
        public Herring? herring;
        public Cod? cod;
        public Tuna? tuna;
        public Halibut? halibut;
        public Eel? eel;
        public Garfish? garfish;
        public GiantOarfish? oarfish;
        private Trawler? trawler = new();

        protected override void Populate()
        {
            Random random = new();

            mackerel = new((uint)random.Next(800, 1000));
            herring = new((uint)random.Next(800, 1000));
            cod = new((uint)random.Next(400, 1000));
            tuna = new((uint)random.Next(300, 1000));
            halibut = new((uint)random.Next(200, 1000));
            eel = new((uint)random.Next(200, 1000));
            garfish = new(25);
            oarfish = new(1);

            LocalFish.AddRange(new List<Fish>() { mackerel, herring, cod, tuna, halibut, eel, garfish, oarfish });

            base.Populate();
        }

        public Ocean()
        {
            Art = GameArt.Ocean;
            Name = "Ocean";
            Description = "The ocean lays before you. " +
            "Your eyes are met with the its insurmountable vastness, the light reflecting on its pellucid waters. " +
            "Something in the distance, resembling a small island catches your eye " +
            "but you quickly discern this object's true nature. " +
            "Horror sets in, as you realise pollution has not spared even this marvel of the natural world. " +
            "There is yet more work to be done.\n\n" +
            "A single trawler belonging to the crew of the boat you're on is working nearby, seemingly expecting you to *talk* to him at any moment, " +
            "or possibly ask for *info*.";
            Character = trawler;

            Populate();
        }
    }

    public class Coast : CleanableLocation
    {
        public Coast(double pollutionUnits) : base(pollutionUnits)
        {
            Art = GameArt.Coast;
            Name = "Coast";
            PollutionType = "Garbage";
            PollutionTypeUnit = "mg/l";
            Description = "You're on the coast. " +
            "It appears that the village's current misfortunes have made their mark " +
            "on the natural world around the settlement. " +
            "Plastic pollutes the once beautiful beach and " +
            "makes the animals' lives an increasingly hard battle for survival each day.";
        }

        override public void DefaultNoCharacters()
        {
            if (LocalCleaners > 0)
                Console.WriteLine("It's kind of hard to talk to someone far off knee deep in the water trying to sift for trash. " +
                "It might be easier to just focus on villager assignment.");
            else
                base.DefaultNoCharacters();
        }
    }

    public class WastePlant : CleanableLocation
    {
        public WastePlant(double pollutionUnits) : base(pollutionUnits)
        {
            Art = GameArt.WastePlant;
            Name = "Wastewater Treatment Plant";
            PollutionType = "Microplastic";
            PollutionTypeUnit = "ppm";
            Description = "You're in the wastewater treatment plant. " +
            "Or what is left of it. " +
            "The empty building's remains loom over the shoreline, its purpose long forgotten.";

            CleanupUnlocked = true; // cannot clean until membrane filter unlocked
        }

        override public void DefaultNoCharacters()
        {
            if (CleanupUnlocked)
                Console.WriteLine("The workers here seem to be too busy for talking. One of them points you towards a bulletin board with an assignment form on it.");
            else
                Console.WriteLine("You try to look around and shout for anyone to respond, but nobody replies. This place is truly abandoned.");
        }
    }
}
