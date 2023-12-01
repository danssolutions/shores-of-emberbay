namespace ShoresOfEmberbay
{
    public sealed class Eel : Fish
    {
        public Eel(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Eel";
            FoodValue = 0.03;
            CatchDifficulty = 0.4;
            BaseReproductionRate = 1.1;
            PollutionSensitivity = 0.8;
        }
    }
}