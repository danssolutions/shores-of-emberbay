namespace TownOfZuul
{
    public sealed class GiantOarfish : Fish
    {
        public GiantOarfish(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Giant Oarfish";
            FoodValue = 10;
            CatchDifficulty = 0.909;
            BaseReproductionRate = 1;
            PollutionSensitivity = 0;
            BycatchOnly = true;
        }
    }
}