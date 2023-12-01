namespace TownOfZuul
{
    public class WastePlant : CleanableLocation
    {
        public string RestoredDescription = "You're in the wastewater treatment plant. " +
            "It appears to be recently refurbished, with workers on site cleaning the plant and maintaining a modern contraption that looks " +
            "like a big water filter.\n" +
            "There is a corkboard on the wall containing *info* as well as a note addressed to you asking to *assign* additional workers here.\n";
        public WastePlant(double pollutionUnits) : base(pollutionUnits)
        {
            Art = GameArt.WastePlant;
            Name = "Wastewater Treatment Plant";
            PollutionType = "Microplastic";
            PollutionTypeUnit = "ppm";
            Description = "You're in the wastewater treatment plant. " +
            "Or what is left of it. " +
            "The empty building's remains loom over the shoreline, its purpose long forgotten.";

            CleanupUnlocked = false; // cannot clean until membrane filter unlocked
        }

        public void UpdateDescription()
        {
            if (CleanupUnlocked)
                Description = RestoredDescription;
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
