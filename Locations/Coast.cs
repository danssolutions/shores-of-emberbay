namespace TownOfZuul
{
    public class Coast : CleanableLocation
    {
        public Coast(double pollutionUnits) : base(pollutionUnits)
        {
            Art = GameArt.Coast;
            Name = "Coast";
            PollutionType = "Garbage";
            PollutionTypeUnit = "pieces per cubic km";
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
}
