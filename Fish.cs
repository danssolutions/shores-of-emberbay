namespace ShoresOfEmberbay
{
    public abstract class Fish
    {
        // If the current population of this species of fish is less than this number, it is immediately treated as endangered.
        protected const uint EndangermentThreshold = 1000;

        public string? Name { get; protected set; }
        // How nutritious this fish is. For example, one fish with a food value of 1.0 can feed 1 villager for 1 day (1/30th of a month).
        public double? FoodValue { get; protected set; }

        // How many fish of this species are swimming about at this moment.
        public uint Population { get; protected set; }
        // Population of this species of fish in the previous month. Used to check if this species is overfished/endangered.
        public uint PreviousPopulation { get; protected set; }

        // How difficult this fish is to catch. Valid values range from 0.0 to 1.0 (larger value = more difficult to catch)
        public double? CatchDifficulty { get; protected set; }

        public string GetCatchDifficultyString()
        {
            if (CatchDifficulty == 1.0)
                return "Impossible";
            else if (CatchDifficulty >= 0.65)
                return "Hard";
            else if (CatchDifficulty >= 0.35)
                return "Medium";
            else
                return "Easy";
        }

        // Fish marked as "bycatch only" cannot be targeted by villagers, and are only caught as a random bonus. Default is false.
        public bool BycatchOnly { get; protected set; }

        // How fast this fish reproduces. A value of 1.1 means the population of this fish increases by 10% every month,
        // assuming no other multipliers are in effect.
        public double BaseReproductionRate { get; protected set; }
        public double ReproductionRate { get; protected set; }
        public double PreviousReproductionRate { get; protected set; }

        // How sensitive this fish is to water pollution/quality. Valid values range from 0.0 to 1.0 (larger value = more sensitive)
        // Repopulation rates of more resilient fish are less impacted by poor water quality, while more sensitive fish are more impacted.
        public double? PollutionSensitivity { get; protected set; }

        public Fish(uint initialPopulation)
        {
            Population = initialPopulation;
            PreviousPopulation = Population;
            BaseReproductionRate = ReproductionRate = PreviousReproductionRate = 0;
            BycatchOnly = false;
        }

        public bool IsEndangered()
        {
            return Population < EndangermentThreshold || Population <= (0.5 * PreviousPopulation) || BaseReproductionRate <= 1.0;
        }

        public uint RemovePopulation(uint catchAmount)
        {
            if (catchAmount > Population)
                Population = 0;
            else
                Population -= catchAmount;
            return Population;
        }

        public void AddPopulation()
        {
            Population = (uint)(Population * ReproductionRate);
        }

        public void SetPreviousPopulation()
        {
            PreviousPopulation = Population;
        }

        public void SetReproductionRates(double waterQuality)
        {
            PreviousReproductionRate = ReproductionRate;
            ReproductionRate = BaseReproductionRate + (-0.5 + waterQuality) * PollutionSensitivity.GetValueOrDefault();
        }
    }
}