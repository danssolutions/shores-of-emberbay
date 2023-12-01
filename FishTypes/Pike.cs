namespace ShoresOfEmberbay
{
    public sealed class Pike : Fish
    {
        public Pike(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Pike";
            FoodValue = 0.03;
            CatchDifficulty = 0.6;
            BaseReproductionRate = 1.15;
            PollutionSensitivity = 0.6;
        }
    }
}