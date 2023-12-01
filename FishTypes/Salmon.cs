namespace TownOfZuul
{
    public sealed class Salmon : Fish
    {
        public Salmon(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Salmon";
            FoodValue = 0.25;
            CatchDifficulty = 0.6;
            BaseReproductionRate = 1.27;
            PollutionSensitivity = 0.8;
        }
    }
}