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
}
