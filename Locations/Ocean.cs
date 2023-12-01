namespace ShoresOfEmberbay
{
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
        private readonly Trawler trawler = new();

        protected override void Populate()
        {
            Random random = new();

            mackerel = new((uint)random.Next(8000, 10000));
            herring = new((uint)random.Next(8000, 10000));
            cod = new((uint)random.Next(4000, 10000));
            tuna = new((uint)random.Next(3000, 10000));
            halibut = new((uint)random.Next(2000, 10000));
            eel = new((uint)random.Next(2000, 10000));
            garfish = new(250);
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
}
