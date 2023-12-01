namespace ShoresOfEmberbay
{
    public sealed class SeaBass : Fish
    {
        public SeaBass(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Sea Bass";
            FoodValue = 0.01;
            CatchDifficulty = 0.7;
            BaseReproductionRate = 1.3;
            PollutionSensitivity = 0.6;
        }
    }
}