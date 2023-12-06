namespace ShoresOfEmberbay
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

        public void UnlockLocation()
        {
            Description = RestoredDescription;
            if (!CleanupUnlocked)
            {
                CleanupUnlocked = true;
                string plantUnlockText = "The wastewater treatment plant has been restored and is now equipped with a membrane filter for microplastic pollution cleanup!\n" +
                "You can now *assign* villagers to work in the plant to clean up the ocean.";
                GenericMenu plantUnlockMenu = new(GameArt.WastePlant, plantUnlockText);
                plantUnlockMenu.Display();
            }
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
