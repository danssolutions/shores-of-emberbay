namespace TownOfZuul
{
    // Base class for all locations in the game.
    // Its properties and methods are available to all classes derived from it.
    public abstract class Location
    {
        private const string NoAssignment = "This location cannot have any villagers assigned to it.";
        public string? Art { get; protected set; }
        public string? Name { get; protected set; }
        public string? Description { get; protected set; }
        public string? Information { get; protected set; }
        public string? Dialogue { get; protected set; }
        public string? Story { get; protected set; }
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

        public virtual void AssignVillagers(uint amount)
        {
            Console.WriteLine(NoAssignment);
        }
    }

    public abstract class FishableLocation : Location
    {
        private const string ZeroAssignment = "Clearing this location of all fishers...";
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

    public abstract class CleanableLocation : Location
    {
        private const string ZeroAssignment = "Clearing this location of all cleaners...";
        private const string ConfirmedAssignment = "Assignment confirmed. \nVillagers ready to clean in this location: ";
        private const string DeniedAssignment = "Cannot assign anyone here, as the village does not have the tools necessary for cleanup.";

        // The amount of pollution currently in the location.
        // For different locations, this represents different types of pollution, measured in its own type of unit.
        public double PollutionCount { get; protected set; }

        // Whether this location can be cleaned by villagers assigned to it.
        // Some locations require additional technology to be unlocked in order for it to be cleanable.
        public bool CleanupUnlocked { get; protected set; } = true;

        public uint LocalCleaners { get; private set; } = new();

        public CleanableLocation(double pollutionUnits)
        {
            PollutionCount = pollutionUnits;
            LocalCleaners = 0;
        }

        public override void AssignVillagers(uint amount)
        {
            if (!CleanupUnlocked)
            {
                Console.WriteLine(DeniedAssignment);
                return;
            }

            if (amount == 0)
            {
                Console.WriteLine(ZeroAssignment);
                LocalCleaners = 0;
                return;
            }

            //TODO: Make sure there are enough "free villagers" that can be assigned

            LocalCleaners = amount;

            //TODO: Update global "free villager" value after this is done, if any exist.

            Console.WriteLine(ConfirmedAssignment + amount + ".");
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
            Art = @"

                                V           ~~
~         ~~          V                                 V
       _T      .,,.    ~--~ ^^          V        -==-
 ^^   // \                    _   ________     
      |[O]    _____,----,-. .' './========\             ~~~
- -/``-|_|- -|.-.-_II__|__ |-----| u    u |- -___ - -- -- _-
\_/_  /   \ _|| | / ''   /'\_|T| |   ||   |\_| u |_ _ _ _/_ 
| | ||--'''' \,--|--..._/,.-{ },  ````````   |u u| U U U U U
| '/__\,.--';|   |[] .-.| O{ _ }     .;.     |+++|+-+-+-+-+-
|  |  | []  -|   '`---.;[,-`\,/  ____________|===|>=== _ _ =
| |[]|,.--'' '',   ''-,.     |  //// ////// /\ T |....| V |.
____________     ;       ''. ' // /// // ///==\
/// ////// /\   /           \ |//////-\////====|      \|/
------------------------------------------------------------
            ";
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
            Art = @"

  -==-              .-.    V      ~--~  _   ~~
        ~--~       /   \              _/ \       V    -==-
      _    ~~  .--'\/\_ \            /    \  V    ___
  V  / \_    _/ V      \/\'__        /\/\  /\  __/   \ V
    /    \  /    .'   _/  /  \     /    \/  \/ .`'\_/\   ~~
   /\/\  /\/ :' __  ^/  ^/    `--./.'  ^  `-.\ _    _:\ _
  /    \/  \  _/  \-' __/.' ^ _  ____  .'\   _/ \ .  __/ \
/\  .-   `. \/     ______________|  |_____ \ /    `._/  ^  \
  `-.__ ^   / .-'./_______________________\`-.  `-. `.  -  `
'`-,   `.  / /     ||__|__||||||||__|__|||    \    \  \  .-/
    `-~---~~--~-~--||__|__||||||||__|__|||~---~~--~~--~-~~`
 ~         `      ~|||||||||||||||||||||||~         ~
    .              ````.~~~-.~~-.~~~..~```    `         .
------------------------------------------------------------
            ";
            Name = "Village Elder's house";
            Description = "You're in the village elder's house. The elder provides you with knowledge on " +
            "how to take care of the population and expand the village. The village elder will provide what you with "
            + "\n what you need to help the village.";
            Dialogue = "Welcome! As you take a look around," +
                " you may notice that this town is not what it used to be." +
                " Let me tell you a story about its past. " +
                "\nType (story) if you wish to continue.";
            Story = "Thank you for for listening Mayor, Let me tell you about how everything changed for the worse " +
            "for everyone in the village. \nWhen I was just a child 60 years ago the village was thriving. "
            + "\nNow we are just trying to survive. Our health i getting worse for everyday, \nbecause we either don't "
            + "get anything to eat or because the fish we eat are contaminated with plastic or other chemicals. "
            + "\nCompanies take our fish so we barely have enough food and we have to be careful deciding what fish to catch. "
            + "\nThey pollute our water and take our fish. They are slowly moving away from our area, "
            + "\nbut now we need to think about what fish we catch and eat fish from polluted water. "
            + "\nWe need you, Mayor. Please help the village become sustainable and make it thrive again.";
            Information = "You can unlock Algae cleaner and water filter and geet it from the Elder, when unlocked.";
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
            Art = @"

__ ___ _            .   :  ;   .    V          ___
,=,_    ``------.__  \        /        .----``   ```--.
    `--,____.------'-   ,--.  -        ``-------`````  V
 .    , \_______-_-~___/____\__~-_-_______/';\._____________
  ~     `\.  V   --       --           .--    \'-.
        . `\._          ~~ --   V      ```\``/``      --
  V      `    `'--..    _--                '      V
             --',.---'   - '  H--,  ___   _^__________^_
    _~__----```` ~.      '__  H  | /  /   |LLLLLLLLLLLL|Y
'```      '  V   /|     /   / H  `/   |   |____,,....__||
        '       /_|_   /   /,_H..------````            `''--
      '  --    \----/ /   ~`|
    -`          ,----/   / /
------------------------------------------------------------
            ";
            Name = "Docks";
            Description = "You're in the village docks.";
            OceanUnlocked = false;
            
            LocalFish.AddRange(new List<Fish>(){seaTrout, seaBass, pike, salmon, sturgeon});
        }
    }

    public class ResearchVessel : CleanableLocation
    {
        // Algae stats go here

        // Code for fetching fish info goes here

        public ResearchVessel(double pollutionUnits) : base(pollutionUnits)
        {
            Art = @"

    .----``   ```--.               ___
    ``-------`````  V           ,=,_    ``------.__
                                    `--,____.------'- 
                                            ,---L--E   _____
                        V   __/___          |   H      |
       V              _____/______|    V    G   H  ____|
             ________/_____\_______\_____.      H``
             \  O O O       __< < <      |\     H     V
___ _ _ ___ __\~__~_ _,_~~_/-/__~~__ __~~|@__ _/H
 =_-_ -=,-T----------T`---/_/-------T----------'__T_________
       /     /              /                   /   ____,---
  -=  /T___________T______         _  /           _|\__/|\__
 - -=~~~-~~=~~~~~~~=~~--,/     -  [X]         -  |X|/  \|/
------------------------------------------------------------
            ";
            Name = "Research Vessel";
            Description = "You're in the research vessel.";
            Information = "Somehow you will be able to see fish stock here in the future.";

            CleanupUnlocked = false; // cannot clean until algae cleaner unlocked
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
            Art = @"

        ...
    .-``   `--.                          ___
    ``---`````                    .----``   ```--.                  
                                   ``-------`````  

                             |                 V
          V              \       /
 V                         .---.        V
                      --  /     \  --               V
`~~^~^~^~^~^~^~^~^~^~^~^-=======-~^~^~^~^~^~^~^~^~^~^~^~^~~`
`~^_~^~^~^~^~-~^_~^~^_~-=========- -~^~^~-~^~^~^~^_~^~^~^~^`
`~^~-~^~^~~^~^~-^~^_~^~~ -=====- ~^~^~-~^~_~^~^~^~^~^~-~^~~`
`~^~^~-~^~~^~^~^~-~^~~-~^~^~-~^~~^-~^~^~^-~^~^~^~^~~~^~^~-^`
------------------------------------------------------------
            ";
            Name = "Ocean";
            Description = "You're in the ocean.";

            LocalFish.AddRange(new List<Fish>(){mackerel, herring, cod, tuna, halibut, eel, garfish, oarfish});
        }
    }

    public class Coast : CleanableLocation
    {
        // Coast trash stats goes here

        public Coast(double pollutionUnits) : base(pollutionUnits)
        {
            Art = @"


                                              \ ' /
_                             V              - ( ) -
--~~,     V       ~~~~                        / , \
     \                           ~~~~~~
-------________ __________ __ _ ___ ___ __ _ _ _____ _ _ ___
                     ---...___ =-= = -_= -=_= _-=_-_ -=- =-_
                              ```--.._= -_= -_= _-=- -_= _=-
                                      ``--._=-_ =-=_-= _-= _
            ~                               ``-._=_-=_- =_-=
                                                 `-._-=_-_=-
                                                     `-._=-_
  `                                             ~        `-.
------------------------------------------------------------
            ";
            Name = "Coast";
            Description = "You're in the coast.";
        }
    }

    public class WastePlant : CleanableLocation
    {
        // Microplastic trash stats goes here

        public WastePlant(double pollutionUnits) : base(pollutionUnits)
        {
            Art = @"

                                           ~~~~~~~~~~~~ 
                             _________          ~~~~
      ~~~                   (---------)
    ~~~~~~~~~~~~             |      _|_       V
          ~~~~               |    /'   `\
                             |   |   H   |              V
                      V      |   |   |--------------|
                           .'    |   ||~~~~~~~~|    |    ,-~
    V                  __/'______|___||__###___|____|_,./
                V     |/  ~         .                `
 _ ___ ___ __ _ _ ___/     
= _-=_-_ -=- =-_  =_//
_-= _-= _ _-_= - _//
------------------------------------------------------------
            ";
            Name = "Wastewater Treatment Plant";
            Description = "You're in the wastewater treatment plant.";

            CleanupUnlocked = false; // cannot clean until membrane filter unlocked
        }
    }
}
