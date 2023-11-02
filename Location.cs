namespace TownOfZuul
{
    // Base class for all locations in the game.
    // Its properties and methods are available to all classes derived from it.
    public abstract class Location
    {
        private const string NoAssignment = "This location cannot have any villagers assigned to it.";
        public string? Name { get; protected set; }
        public string? Description { get; protected set; }
        public string? Information { get; protected set; }
        public string? Dialogue { get; protected set; }
        public Dictionary<string, Location> Exits { get; private set; } = new();

        public void SetExits(Location? north, Location? east, Location? south, Location? west)
        {
            SetExit("north", north);
            SetExit("east", east);
            SetExit("south", south);
            SetExit("west", west);
        }
        //is it working?
        public void SetExit(string direction, Location? neighbor)
        {
            if (neighbor != null)
                Exits[direction] = neighbor;
        }

        public virtual void AssignVillagers(uint amount)
        {
            Console.WriteLine(NoAssignment);
        }
    }

    public abstract class FishableLocation : Location
    {
        private const string ZeroAssignment = "Clearing this location of all assignments...";
        public List<Fish> LocalFish { get; private set; } = new();
        public List<uint> LocalFishers { get; private set; } = new();

        public override void AssignVillagers(uint amount)
        {
            if (amount == 0)
            {
                Console.WriteLine(ZeroAssignment);
                LocalFishers.Clear();
                return;
            }

            //TODO: Make sure there are enough "free villagers" that can be assigned

            FishingMenu fishMenu = new(this,amount);
            fishMenu.Display();

            LocalFishers = fishMenu.GetFisherList(LocalFishers);

            //TODO: Update global "free villager" value after this is done, if any exist.
        }

        public double GetBiodiversityScore()
        {
            return 2.0; // TODO: replace with something meaningful
        }
    }

    // TODO: put classes below in separate files
    public class Village : Location
    {
        public uint PopulationCount { get; private set; }
        public double PopulationHealth { get; private set; }
        public double FoodUnits { get; private set; }

        public Village()
        {
            Name = "Village";
            Description = "You're in the village.";
            PopulationCount = 5;
            PopulationHealth = 90.0;
            FoodUnits = 10.0;

            Information = $"Current population is: {PopulationCount}. Population health: {PopulationHealth}. Avaible food units: {FoodUnits}.";
        }
    }

    public class ElderHouse : Location
    {
        public bool AlgaeCleanerUnlocked { get; private set; }
        public bool WaterFilterUnlocked { get; private set; }

        public ElderHouse()
        {
            Name = "Village Elder's house";
            Description = "You're in the village elder's house.";
            Dialogue = "Welcome! As you take a look around," +
                " you may notice that this town is not what it used to be." +
                " Let me tell you a story about its past. " +
                "Type (_) you wish to continue (idk, how we want this to work etc).";
            AlgaeCleanerUnlocked = false;
            WaterFilterUnlocked = false;
        }
    }

    public class Docks : FishableLocation
    {
        public SeaTrout seaTrout = new(500);
        public SeaBass seaBass = new(500);
        public Pike pike = new(500);
        public Salmon salmon = new(500);
        public Sturgeon sturgeon = new(500);

        public bool OceanUnlocked { get; private set; }
        public Docks()
        {
            Name = "Docks";
            Description = "You're in the village docks.";
            OceanUnlocked = false;
            
            LocalFish.AddRange(new List<Fish>(){seaTrout, seaBass, pike, salmon, sturgeon});
        }
    }

    public class ResearchVessel : Location
    {
        // Algae stats go here

        // Code for fetching fish info goes here

        public ResearchVessel()
        {
            Name = "Research Vessel";
            Description = "You're in the research vessel.";
            Information = "Somehow you will be able to see fish stock here in the future.";
        }
    }

    public class Ocean : FishableLocation
    {
        public Mackerel mackerel = new(500);
        public Herring herring = new(500);
        public Cod cod = new(500);
        public Tuna tuna = new(500);
        public Halibut halibut = new(500);
        public Eel eel = new(500);
        public Garfish garfish = new(500);
        public GiantOarfish oarfish = new(500);

        public Ocean()
        {
            Name = "Ocean";
            Description = "You're in the ocean.";

            LocalFish.AddRange(new List<Fish>(){mackerel, herring, cod, tuna, halibut, eel, garfish, oarfish});
        }
    }

    public class Coast : Location
    {
        // Coast trash stats goes here

        public Coast()
        {
            Name = "Coast";
            Description = "You're in the coast.";
        }
    }

    public class WastePlant : Location
    {
        // Microplastic trash stats goes here

        public WastePlant()
        {
            Name = "Wastewater Treatment Plant";
            Description = "You're in the wastewater treatment plant.";
        }
    }
}
