namespace TownOfZuul
{
    public sealed class Cod : Fish
    {
        public Cod(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Cod";
            FoodValue = 0.2;
            CatchDifficulty = 0.5;
            BaseReproductionRate = 1.155;
            PollutionSensitivity = 0.6;
        }
    }
}