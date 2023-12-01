namespace TownOfZuul
{
    public class Village : Location
    {
        public int PopulationCount { get; private set; }
        public uint FreeVillagers { get; private set; }
        public double PopulationHealth { get; private set; }
        public double FoodUnits { get; private set; }
        public Village()
        {
            Art = GameArt.Village;
            Name = "Village";
            Description = "You're in the village." +
            " Once a large and prosperous place, you can easily tell its glory days are in the past." +
            " Most of the buildings, which used to provide the shelter and livelihood to numerous people " +
            "are now desolate and ill-kept." +
            " Somehow, though, you can feel that this village might get another shot at prosperity.\n" +
            "There is an *info* board in the main village square.";
            PopulationCount = 10;
            FreeVillagers = 10;
            PopulationHealth = 0.5;
            FoodUnits = 15.0;
        }

        override public void GetLocationInfo()
        {
            Console.WriteLine("Population count: " + PopulationCount);
            Console.WriteLine("Villagers free for assignment: " + FreeVillagers);
            Console.WriteLine("Population health: " + Math.Round(PopulationHealth * 100, 2) + "%");
            Console.WriteLine("Current food stock: " + FoodUnits + " monthly ration" + (FoodUnits == 1 ? "" : "s"));
        }

        public void SetFreeVillagers(uint amount)
        {
            FreeVillagers = amount;
        }

        public void AddToFoodStock(double? additionalFood)
        {
            FoodUnits += additionalFood.GetValueOrDefault();
        }
        public double ConsumeFoodStock(double? foodAmount)
        {
            FoodUnits -= foodAmount.GetValueOrDefault();
            double leftovers = FoodUnits;
            if (FoodUnits < 0)
                FoodUnits = 0;
            return leftovers;
        }
        public void UpdatePopulation(double waterQuality)
        {
            double leftovers = ConsumeFoodStock(PopulationCount);
            // Population health is updated dependent on food, water quality
            if (leftovers < 0)
                SetPopulationHealth(1.0 + (leftovers / PopulationCount));
            else
                SetPopulationHealth(1.5); //improves by 50% if all food needs are met
            // Health naturally decreases when water quality < 30%, and improves (slowly) when water quality goes up
            SetPopulationHealth(0.7 + 0.5 * (waterQuality - 0.3));
            // Population count is updated based on food stock, and existing health
            int newVillagers = (int)(leftovers * (leftovers >= 0 ? PopulationHealth : 1.0));
            if (newVillagers > 50)
                newVillagers = 50;
            PopulationCount += newVillagers;
            if (PopulationCount <= 0)
            {
                PopulationCount = 0;
                SetPopulationHealth(0);
            }
        }
        public void SetPopulationHealth(double multiplier)
        {
            PopulationHealth *= multiplier;
            if (PopulationHealth <= 0.0)
                PopulationHealth = 0.0;
            else if (PopulationHealth > 1.0)
                PopulationHealth = 1.0;
        }
        override public void DefaultNoCharacters()
        {
            Console.WriteLine("You try to talk to random villagers, but none of them seem very talkative.");
        }
    }
}
