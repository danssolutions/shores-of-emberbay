namespace ShoresOfEmberbay
{
    public sealed class Garfish : Fish
    {
        public Garfish(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Garfish";
            FoodValue = 0.12;
            CatchDifficulty = 0.875;
            BaseReproductionRate = 1.05;
            PollutionSensitivity = 0;
            BycatchOnly = true;
        }
    }
}