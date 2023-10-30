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

    public class Docks : Location
    {
        public bool OceanUnlocked { get; private set; }

        // Docks fish stats go here
        public Docks()
        {
            Name = "Docks";
            Description = "You're in the village docks.";
            OceanUnlocked = false;
        }

        public override void AssignVillagers(uint amount)
        {
            /*
            The flow for this (and other assignable locations) should go like this:
            - Make sure amount > 0
            - Make sure there are enough "free villagers" that can be assigned
            - If location is fishable:
                - Provide fishing menu to user
                - User assigns villagers to individual fish
                - Make sure the total villager amount in menu doesn't exceed user's allocated villager amount
            - Else:
                - Assign user's allocated villager amount to location
            - Update location's villager values
            - Update "free villager" value, make sure it doesn't exceed total pop count
            */
            Console.WriteLine("You've assigned. I guess.");
        }

        // TODO: there needs to be a method for unassigning villagers. This would be easy for cleaning location, but what about fishable locations?

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
            Information = "Somehow you will be able to see fish stock here in the future.";
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
