namespace TownOfZuul
{
    public sealed class SeaTrout : Fish
    {
        public SeaTrout(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Sea Trout";
            FoodValue = 0.1;
            CatchDifficulty = 0.6;
            BaseReproductionRate = 1.3;
            PollutionSensitivity = 0.5;
        }
    }
}