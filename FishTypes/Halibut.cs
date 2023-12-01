namespace ShoresOfEmberbay
{
    public sealed class Halibut : Fish
    {
        public Halibut(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Halibut";
            FoodValue = 0.05;
            CatchDifficulty = 0.7;
            BaseReproductionRate = 1.3;
            PollutionSensitivity = 0.7;
        }
    }
}