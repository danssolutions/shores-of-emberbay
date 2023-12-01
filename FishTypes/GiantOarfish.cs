namespace ShoresOfEmberbay
{
    // secret giant fish
    public sealed class GiantOarfish : Fish
    {
        public GiantOarfish(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Giant Oarfish";
            FoodValue = 1;
            CatchDifficulty = 0.99;
            BaseReproductionRate = 1;
            PollutionSensitivity = 0;
            BycatchOnly = true;
        }
    }
}