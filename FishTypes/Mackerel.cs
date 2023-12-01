namespace ShoresOfEmberbay
{
    public sealed class Mackerel : Fish
    {
        public Mackerel(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Mackerel";
            FoodValue = 0.05;
            CatchDifficulty = 0.1;
            BaseReproductionRate = 1.5;
            PollutionSensitivity = 0.65;
        }
    }
}