namespace ShoresOfEmberbay
{
    public class ResearchVessel : CleanableLocation
    {
        private readonly Scientist scientist = new();
        public string RestoredDescription = "\nYou can also *assign* villagers here to assist the crew with phosphorus cleanup.";
        public ResearchVessel(double pollutionUnits) : base(pollutionUnits)
        {
            Art = GameArt.ResearchVessel;
            Name = "Research Vessel";
            PollutionType = "Phosphorus";
            PollutionTypeUnit = "ug/l";
            Description = "You're in the research vessel. " +
            "You are greeted by the sight of somewhat modern technology and machinery, " +
            "some of which can be considered a rare find nowadays. " +
            "How such equipment has remained so well-maintained to this day is a mystery to you " +
            "but you are nevertheless impressed by its condition. " +
            "If this village and its surroundings are going to be saved, " +
            "you can already tell this ship will be instrumental in achieving that.\n" +
            "A scientist nearby seems to be focused on some sort of document. You might be able to *talk* to him or ask for *info*.";

            CleanupUnlocked = false; // cannot clean until nutrient cleaner unlocked
            Character = scientist;
        }

        public void UnlockLocation()
        {
            Description += RestoredDescription;
            if (!CleanupUnlocked)
            {
                CleanupUnlocked = true;
                string nutrientUnlockText = "The research vessel is now equipped with tools for phosphorus pollution cleanup!\n" +
                "You can now *assign* villagers to help the crew and scientists clean up the ocean.";
                GenericMenu nutrientUnlockMenu = new(GameArt.ResearchVessel, nutrientUnlockText);
                nutrientUnlockMenu.Display();
            }
        }

        private static void ShowWaterQuality(double waterQuality)
        {
            Console.WriteLine("Water quality: " + Math.Round(waterQuality * 100, 2) + "% pure");
        }
        public static void ShowResearchStats(List<FishableLocation> fishableLocations, List<CleanableLocation> cleanableLocations, double waterQuality)
        {
            Console.WriteLine();
            foreach (CleanableLocation cleanableLocation in cleanableLocations)
            {
                Console.WriteLine($"{cleanableLocation.PollutionType} pollution: " + (int)cleanableLocation.PollutionCount + " " + cleanableLocation.PollutionTypeUnit);
                //Console.WriteLine($"{cleanableLocation.PollutionType} initial pollution: " + cleanableLocation.InitialPollution + " " + cleanableLocation.PollutionTypeUnit);
            }
            ShowWaterQuality(waterQuality);
            Console.WriteLine();
            foreach (FishableLocation fishableLocation in fishableLocations)
            {
                Console.WriteLine($"Total fish in the {fishableLocation.Name}: " + fishableLocation.LocalFish.Sum(item => item.Population));
                foreach (Fish fish in fishableLocation.LocalFish)
                {
                    if (fish.BycatchOnly == false)
                        Console.WriteLine("- " + fish.Name + ": " + fish.Population + " (previously " + fish.PreviousPopulation + ")");
                }
                Console.WriteLine();
                foreach (Fish fish in fishableLocation.LocalFish)
                {
                    double reproductionRate = fish.ReproductionRate != 0 ? fish.ReproductionRate : fish.BaseReproductionRate;
                    double previousReproductionRate = fish.PreviousReproductionRate != 0 ? fish.PreviousReproductionRate : fish.BaseReproductionRate;
                    if (fish.BycatchOnly == false)
                        Console.WriteLine("- " + fish.Name + " reproduction rate: " + Math.Round(reproductionRate, 2) + " (previously " + Math.Round(previousReproductionRate, 2) + ")");
                }
                Console.WriteLine();
            }
        }
    }
}
