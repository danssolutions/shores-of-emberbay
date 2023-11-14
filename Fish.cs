namespace TownOfZuul
{
    public abstract class Fish
    {
        // If the current population of this species of fish is less than this number, it is immediately treated as endangered.
        protected const uint EndangermentThreshold = 500;

        public string? Name { get; protected set; }
        // How nutritious this fish is. For example, one fish with a food value of 1.0 can feed 1 villager for 1 day (1/30th of a month).
        public double? FoodValue { get; protected set; }

        // How many fish of this species are swimming about at this moment.
        public uint Population { get; protected set; }
        // Population of this species of fish in the previous month. Used to check if this species is overfished/endangered.
        public uint PreviousPopulation { get; protected set; }

        // How difficult this fish is to catch. Valid values range from 0.0 to 1.0 (larger value = more difficult to catch)
        public double? CatchDifficulty { get; protected set; }
        
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
            ReproductionRate = BaseReproductionRate * (1.0 - (PollutionSensitivity.GetValueOrDefault() * waterQuality)); // TODO: add biodiversity score, possibly?
        }
    }

    // TODO: update food values (so it's in comparison to 1FU/mo per villager), update difficulty so player is more reliant on bycatch

    public sealed class SeaTrout : Fish
    {
        public SeaTrout(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Sea Trout";
            FoodValue = 0.9;
            CatchDifficulty = 0.6;
            BaseReproductionRate = 1.575;
            PollutionSensitivity = 0.5;
        }
    }

    public sealed class SeaBass : Fish
    {
        public SeaBass(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Sea Bass";
            FoodValue = 0.8;
            CatchDifficulty = 0.7;
            BaseReproductionRate = 1.6;
            PollutionSensitivity = 0.4;
        }
    }

    public sealed class Pike : Fish
    {
        public Pike(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Pike";
            FoodValue = 0.9;
            CatchDifficulty = 0.7;
            BaseReproductionRate = 1.65;
            PollutionSensitivity = 0.4;
        }
    }

    public sealed class Salmon : Fish
    {
        public Salmon(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Salmon";
            FoodValue = 1.2;
            CatchDifficulty = 0.6;
            BaseReproductionRate = 1.7;
            PollutionSensitivity = 0.2;
        }
    }

    public sealed class Sturgeon : Fish
    {
        public Sturgeon(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Sturgeon";
            FoodValue = 1.5;
            CatchDifficulty = 0.8;
            BaseReproductionRate = 1.25;
            PollutionSensitivity = 0.1;
        }
    }

    public sealed class Mackerel : Fish
    {
        public Mackerel(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Mackerel";
            FoodValue = 0.25;
            CatchDifficulty = 0.3;
            BaseReproductionRate = 1.75;
            PollutionSensitivity = 0.35;
        }
    }

    public sealed class Herring : Fish
    {
        public Herring(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Herring";
            FoodValue = 0.2;
            CatchDifficulty = 0.2;
            BaseReproductionRate = 1.8;
            PollutionSensitivity = 0.2;
        }
    }

    public sealed class Cod : Fish
    {
        public Cod(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Cod";
            FoodValue = 1;
            CatchDifficulty = 0.5;
            BaseReproductionRate = 1.55;
            PollutionSensitivity = 0.4;
        }
    }

    public sealed class Tuna : Fish
    {
        public Tuna(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Tuna";
            FoodValue = 8;
            CatchDifficulty = 0.5;
            BaseReproductionRate = 2.1;
            PollutionSensitivity = 0.5;
        }
    }

    public sealed class Halibut : Fish
    {
        public Halibut(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Halibut";
            FoodValue = 4;
            CatchDifficulty = 0.6;
            BaseReproductionRate = 1.55;
            PollutionSensitivity = 0.3;
        }
    }

    public sealed class Eel : Fish
    {
        public Eel(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Eel";
            FoodValue = 2;
            CatchDifficulty = 0.4;
            BaseReproductionRate = 1.65;
            PollutionSensitivity = 0.2;
        }
    }

    public sealed class Garfish : Fish
    {
        public Garfish(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Garfish";
            FoodValue = 1.2;
            CatchDifficulty = 0.875;
            BaseReproductionRate = 1.05;
            PollutionSensitivity = 0;
            BycatchOnly = true;
        }
    }

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