namespace TownOfZuul
{
    public abstract class Location
    {
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
            Console.WriteLine("This location cannot have any villagers assigned to it.");
        }
    }

    public abstract class FishableLocation : Location
    {
        public List<Fish> LocalFish { get; private set; } = new();
        public List<uint> LocalFishers { get; protected set; } = new();

        public override void AssignVillagers(uint amount)
        {
            if (amount == 0)
            {
                Console.WriteLine("Clearing this location of all fishers...");
                LocalFishers.Clear();
                return;
            }

            //TODO: Make sure there are enough "free villagers" that can be assigned

            FishingMenu fishMenu = new(this, amount);
            fishMenu.Display();

            LocalFishers = fishMenu.GetFisherList(LocalFishers);

            //TODO: Update global "free villager" value after this is done, if any exist.
        }

        public double GetBiodiversityScore()
        {
            return 2.0; // TODO: replace with something meaningful
        }

        public void CatchFish()
        {
            uint catchAmount;
            uint bycatchAmount;

            Random random = new();

            for (int fishType = 0; fishType < LocalFish.Count; fishType++) // for each type of fish in ocean
            {
                LocalFish[fishType].SetPreviousPopulation();
                for (uint i = 0; i < LocalFishers[fishType]; i++) // for each fisher catching a specific fish type
                {
                    catchAmount = (uint)(random.Next(30, 200) * (1.0 - LocalFish[fishType].CatchDifficulty.GetValueOrDefault()));

                    if (catchAmount > LocalFish[fishType].Population)
                        catchAmount = LocalFish[fishType].Population;

                    Game.CurrentGameInstance?.AddToFoodStock((LocalFish[fishType].CatchDifficulty *catchAmount)*LocalFish[fishType].CatchDifficulty);
                    // (fish dif* catch amount)* catch diff

                    LocalFish[fishType].RemovePopulation(catchAmount);

                    // try for bycatch: iterate through random fish in this location and attempt to catch any one of them
                    foreach (Fish bycatch in LocalFish) // for each type of fish in docks
                    {
                        bycatchAmount = (uint)(random.Next(1, 12) * random.NextDouble() * (1.0 - bycatch.CatchDifficulty.GetValueOrDefault()));

                        if (bycatchAmount > bycatch.Population)
                            bycatchAmount = bycatch.Population;

                        bycatch.RemovePopulation(bycatchAmount);

                        // pause for dramatic effect, for we caught an ultra rare fish (temporary)
                        if (bycatchAmount > 0 && bycatch.Name == "Giant Oarfish")
                        {
                            Console.WriteLine("Woah, a villager caught a rare " + bycatch.Name + "!");
                            Thread.Sleep(2000);
                        }
                        Game.CurrentGameInstance?.AddToFoodStock(bycatch.FoodValue);
                        // TODO: move AddToFoodStock to more suitable location
                        //village?.AddToFoodStock(bycatch.FoodValue);
                    }

                    //village?.AddToFoodStock(fishableLocation?.LocalFish[fishType].FoodValue);
                }
            }
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
    }

    public abstract class CleanableLocation : Location
    {
        // The amount of pollution currently in the location.
        // For different locations, this represents different types of pollution, measured in its own type of unit.
        public readonly double InitialPollution;
        public double PollutionCount { get; protected set; }

        // Whether this location can be cleaned by villagers assigned to it.
        // Some locations require additional technology to be unlocked in order for it to be cleanable.
        public bool CleanupUnlocked { get; protected set; } = true;

        public uint LocalCleaners { get; private set; } = new();

        public CleanableLocation(double pollutionUnits)
        {
            InitialPollution = PollutionCount = pollutionUnits;
            LocalCleaners = 0;
        }

        public override void AssignVillagers(uint amount)
        {
            if (!CleanupUnlocked)
            {
                Console.WriteLine("Cannot assign anyone here, as the village does not have the tools necessary for cleanup.");
                return;
            }

            if (amount == 0)
            {
                Console.WriteLine("Clearing this location of all cleaners...");
                LocalCleaners = 0;
                return;
            }

            //TODO: Make sure there are enough "free villagers" that can be assigned

            LocalCleaners = amount;

            //TODO: Update global "free villager" value after this is done, if any exist.

            Console.WriteLine("Assignment confirmed. \nVillagers ready to clean in this location: " + amount + ".");
        }

        public void CleanPollution()
        {
            Random random = new();
            PollutionCount -= LocalCleaners * random.NextDouble();
            if (PollutionCount < 0)
                PollutionCount = 0;
        }
    }

    // TODO: put classes below in separate files
    public sealed class Village : Location
    {
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
            Description = "You're in the village." +
            " Once a large and prosperous place, you can easily tell its glory days are in the past." +
            " Most of the buildings, which used to provide the shelter and livelihood to numerous people " +
            "are now desolate and ill-kept." +
            " Somehow, though, you can feel that this village might get another shot at prosperity.";

            //Information = $"Current population is: {PopulationCount}. Population health: {PopulationHealth}. Avaible food units: {FoodUnits}.";
            Information = $"Current population is whatever.";
        }
    }

    public sealed class ElderHouse : Location
    {
        public bool AlgaeCleanerUnlocked { get; private set; }
        public bool WaterFilterUnlocked { get; private set; }
        public double PopulationHealth { get; private set; }

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
            PopulationHealth = 90.0;
            Name = "Village Elder's house";
            Description = "On the outskirts of town you find yourself looking at a small but well-maintained wooden shack." +
            " Although it is as old as most of the surrounding architecture," +
            " the passage of time has not managed to tear down this testament of the village's past greatness." +
            " You're in front of the village elder's house.\n The elder provides you with knowledge on " +
            "how to take care of the population and expand the village. The village elder will provide what you with "
            + "\n what you need to help the village.";
            Story = "Thank you for for listening Mayor, Let me tell you about how everything changed for the worse " +
            "for everyone in the village. \nWhen I was just a child 60 years ago the village was thriving. "
            + "\nNow we are just trying to survive. Our health i getting worse for everyday, \nbecause we either don't "
            + "get anything to eat or because the fish we eat are contaminated with plastic or other chemicals. "
            + "\nCompanies take our fish so we barely have enough food and we have to be careful deciding what fish to catch. "
            + "\nThey pollute our water and take our fish. They are slowly moving away from our area, "
            + "\nbut now we need to think about what fish we catch and eat fish from polluted water. "
            + "\nWe need you, Mayor. Please help the village become sustainable and make it thrive again.";
            Information = "You can unlock algae cleaner and water filter and get it from the Elder, when unlocked. "
            + "\nYou need to increase population health to more than 90 to unlock algae cleaner, then talk to the elder to get it.";
            AlgaeCleanerUnlocked = false;
            WaterFilterUnlocked = false;

            if (PopulationHealth > 90)
                Dialogue = "Great job! You have unlocked algae cleaner. Type (algae) to get the algae cleaner.";
            else
                Dialogue = "Welcome! As you take a look around," +
                " you may notice that this town is not what it used to be." +
                " Let me tell you a story about its past. " +
                "\nType (story) if you wish to continue.";
        }
    }

    public sealed class Docks : FishableLocation
    {
        public SeaTrout? seaTrout;
        public SeaBass? seaBass;
        public Pike? pike;
        public Salmon? salmon;
        public Sturgeon? sturgeon;

        public bool OceanUnlocked { get; private set; }

        private void Populate()
        {
            Random random = new();

            seaTrout = new((uint)random.Next(200, 1000));
            seaBass = new((uint)random.Next(200, 1000));
            pike = new((uint)random.Next(200, 1000));
            salmon = new((uint)random.Next(200, 1000));
            sturgeon = new((uint)random.Next(200, 1000));

            LocalFish.AddRange(new List<Fish>() { seaTrout, seaBass, pike, salmon, sturgeon });

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
            Description = "You're at the village docks. " +
            "A place where many of the village people's found employment now lies empty, " +
            "save for the odd boat or seagull. " +
            "A large chunk of the construction has been taken by the sea and the storms throughout the years, " +
            "some of it still floating on the water, rocking with the waves. " +
            "Even still, the view of the waterfront remains as impressive as it has always been.";
            OceanUnlocked = false;

            Populate();
        }

        public bool IsOceanUnlocked(int population = 0)
        {
            if (!OceanUnlocked)
            {
                OceanUnlocked = population > 400;
            }
            return OceanUnlocked;
        }
    }

    public sealed class ResearchVessel : CleanableLocation
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
            Description = "You're in the research vessel. " +
            "You are greeted by the sight of somewhat modern technology and machinery, " +
            "some of which can be concidered a rare find nowadays. " +
            "How such equipment has remained so well-maintained to this day is a mystery to you " +
            "but you are nevertheless impressed by its condition. " +
            "If this village and its surroundings are going to be saved, " +
            "you can already tell this ship will be instrumental in achieving that.";
            Information = "Somehow you will be able to see fish stock here in the future.";

            CleanupUnlocked = false; // cannot clean until algae cleaner unlocked
        }
    }

    public sealed class Ocean : FishableLocation
    {
        public Mackerel? mackerel;
        public Herring? herring;
        public Cod? cod;
        public Tuna? tuna;
        public Halibut? halibut;
        public Eel? eel;
        public Garfish? garfish;
        public GiantOarfish? oarfish;

        private void Populate()
        {
            Random random = new();

            mackerel = new((uint)random.Next(200, 1000));
            herring = new((uint)random.Next(200, 1000));
            cod = new((uint)random.Next(200, 1000));
            tuna = new((uint)random.Next(200, 1000));
            halibut = new((uint)random.Next(200, 1000));
            eel = new((uint)random.Next(200, 1000));
            garfish = new(25);
            oarfish = new(3);

            LocalFish.AddRange(new List<Fish>() { mackerel, herring, cod, tuna, halibut, eel, garfish, oarfish });

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
            Description = "The ocean lays before you. " +
            "Your eyes are met with the its insurmountable vastness, the light reflecting on its pellucid waters. " +
            "Something in the distance, resembling a small island catches your eye " +
            "but you quickly discern this object's true nature. " +
            "Horror sets in, as you realise pollution has not spared even this marvel of the natural world. " +
            "There is yet more work to be done.";

            Populate();
        }
    }

    public sealed class Coast : CleanableLocation
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
            Description = "You're on the coast. " +
            "It appears that the village's current misfortunes have made their mark " +
            "on the natural world around the settlement. " +
            "Plastic pollutes the once beautiful beach and " +
            "makes the animals' lives an increasingly hard battle for survival each day.";
        }
    }

    public sealed class WastePlant : CleanableLocation
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
            Description = "You're in the wastewater treatment plant. " +
            "Or what is left of it. " +
            "The empty building's remains loom over the shoreline, its purpose long forgotten.";

            CleanupUnlocked = false; // cannot clean until membrane filter unlocked
        }
    }
}
