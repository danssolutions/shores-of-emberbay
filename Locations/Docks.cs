namespace ShoresOfEmberbay
{
    public class Docks : FishableLocation
    {
        public SeaTrout? seaTrout;
        public SeaBass? seaBass;
        public Pike? pike;
        public Salmon? salmon;
        public Sturgeon? sturgeon;
        private readonly Fisherman? fisherman = new();

        public bool OceanUnlocked { get; private set; }

        override protected void Populate()
        {
            Random random = new();

            seaTrout = new((uint)random.Next(5000, 10000));
            seaBass = new((uint)random.Next(5000, 10000));
            pike = new((uint)random.Next(3000, 5000));
            salmon = new((uint)random.Next(3000, 10000));
            sturgeon = new((uint)random.Next(2000, 5000));

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
}
