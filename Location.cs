namespace TownOfZuul
{
    // Base class for all locations in the game.
    // Its properties and methods are available to all classes derived from it.
    public abstract class Location
    {
        public string? Name { get; protected set; }
        public string? Description { get; protected set; }
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
    }

    public interface IFishable
    {
        // Note: add more stuff here later, like returning a list of all fish types in the location
        public double GetBiodiversityScore();
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
            AlgaeCleanerUnlocked = false;
            WaterFilterUnlocked = false;
        }
    }

    public class Docks : Location, IFishable
    {
        public bool OceanUnlocked { get; private set; }

        // Docks fish stats go here
        public Docks()
        {
            Name = "Docks";
            Description = "You're in the village docks.";
            OceanUnlocked = false;
        }

        public double GetBiodiversityScore()
        {
            return 2.0; // TODO: replace with something meaningful
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
        }
    }

    public class Ocean : Location, IFishable
    {
        // Ocean fish stats go here

        public Ocean()
        {
            Name = "Ocean";
            Description = "You're in the ocean.";
        }

        public double GetBiodiversityScore()
        {
            return 2.0; // TODO: replace with something meaningful
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
