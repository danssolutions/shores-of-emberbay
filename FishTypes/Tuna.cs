namespace TownOfZuul
{
    public sealed class Tuna : Fish
    {
        public Tuna(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Tuna";
            FoodValue = 0.9;
            CatchDifficulty = 0.7;
            BaseReproductionRate = 1.2;
            PollutionSensitivity = 0.5;
        }
    }
}