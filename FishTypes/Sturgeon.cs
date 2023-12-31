namespace ShoresOfEmberbay
{
    public sealed class Sturgeon : Fish
    {
        public Sturgeon(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Sturgeon";
            FoodValue = 0.1;
            CatchDifficulty = 0.9;
            BaseReproductionRate = 1.10;
            PollutionSensitivity = 0.9;
        }
    }
}