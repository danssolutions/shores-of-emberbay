namespace ShoresOfEmberbay
{
    public sealed class Herring : Fish
    {
        public Herring(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Herring";
            FoodValue = 0.005;
            CatchDifficulty = 0.1;
            BaseReproductionRate = 1.5;
            PollutionSensitivity = 0.8;
        }
    }
}