namespace TownOfZuul
{
    public abstract class Fish
    {
        // If the current population of this species of fish is less than this number, it is immediately treated as endangered.
        protected const uint EndangermentThreshold = 500;

        public string? Name { get; protected set; }
        // How nutritious this fish is. For example, one fish with a food value of 1.0 can feed 1 villager for 1 day (1/30th of a month).
        public double? FoodValue
        {
            get
            {
                return FoodValue;
            }
            protected set
            {
                if (value < 0.0)
                {
                    throw new ArgumentOutOfRangeException(nameof(FoodValue));
                }
            }
        }

        // How many fish of this species are swimming about at this moment.
        public uint CurrentPopulation { get; protected set; }
        // Population of this species of fish in the previous month. Used to check if this species is overfished/endangered.
        public uint PreviousPopulation { get; protected set; }

        // How difficult this fish is to catch. Valid values range from 0.0 to 1.0 (larger value = more difficult to catch)
        public double? CatchDifficulty
        {
            get
            {
                return CatchDifficulty;
            }
            protected set
            {
                if (value < 0.0 || value > 1.0)
                {
                    throw new ArgumentOutOfRangeException(nameof(CatchDifficulty));
                }
            }
        }
        // Fish marked as "bycatch only" cannot be targeted by villagers, and are only caught as a random bonus. Default is false.
        public bool BycatchOnly { get; protected set; }

        // How fast this fish reproduces. A value of 1.1 means the population of this fish increases by 10% every month,
        // assuming no other multipliers are in effect.
        public double? BaseReproductionRate
        {
            get
            {
                return BaseReproductionRate;
            }
            protected set
            {
                if (value < 0.0)
                {
                    throw new ArgumentOutOfRangeException(nameof(BaseReproductionRate));
                }
            }
        }
        // How resilient this fish is to water pollution/quality. Valid values range from 0.0 to 1.0 (larger value = more resilient)
        // Repopulation rates of more resilient fish are less impacted by poor water quality, while more sensitive fish are more impacted.
        public double? PollutionResilience
        {
            get
            {
                return PollutionResilience;
            }
            protected set
            {
                if (value < 0.0 || value > 1.0)
                {
                    throw new ArgumentOutOfRangeException(nameof(PollutionResilience));
                }
            }
        }

        // TODO: reproRate = BaseReproductionRate * PollutionResilience * biodiversityScore

        public Fish(uint initialPopulation)
        {
            CurrentPopulation = initialPopulation;
            PreviousPopulation = 0;
            BycatchOnly = false;
        }

        public bool IsEndangered()
        {
            return CurrentPopulation < EndangermentThreshold || CurrentPopulation <= (0.5 * PreviousPopulation) || BaseReproductionRate <= 1.0;
        }
    }

    public class SeaTrout : Fish
    {
        public SeaTrout(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Sea Trout";
            FoodValue = 0.5;
            CatchDifficulty = 0.5;
            BaseReproductionRate = 3.5;
            PollutionResilience = 0.5;
        }
    }

    public class SeaBass : Fish
    {
        public SeaBass(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Sea Bass";
            FoodValue = 0.75;
            CatchDifficulty = 0.5;
            BaseReproductionRate = 0.5;
            PollutionResilience = 0.5;
        }
    }

    public class Pike : Fish
    {
        public Pike(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Pike";
            FoodValue = 0.6;
            CatchDifficulty = 0.5;
            BaseReproductionRate = 1.5;
            PollutionResilience = 0.5;
        }
    }

    public class Salmon : Fish
    {
        public Salmon(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Salmon";
            FoodValue = 0.9;
            CatchDifficulty = 0.5;
            BaseReproductionRate = 1.5;
            PollutionResilience = 0.5;
        }
    }

    public class Sturgeon : Fish
    {
        public Sturgeon(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Salmon";
            FoodValue = 2;
            CatchDifficulty = 0.5;
            BaseReproductionRate = 1;
            PollutionResilience = 0.5;
        }
    }

    public class Mackerel : Fish
    {
        public Mackerel(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Mackerel";
            FoodValue = 0.25;
            CatchDifficulty = 0.5;
            BaseReproductionRate = 4.2;
            PollutionResilience = 0.5;
        }
    }

    public class Herring : Fish
    {
        public Herring(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Herring";
            FoodValue = 0.3;
            CatchDifficulty = 0.5;
            BaseReproductionRate = 3.8;
            PollutionResilience = 0.25;
        }
    }

    public class Cod : Fish
    {
        public Cod(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Cod";
            FoodValue = 0.8;
            CatchDifficulty = 0.5;
            BaseReproductionRate = 1.8;
            PollutionResilience = 0.5;
        }
    }

    public class Tuna : Fish
    {
        public Tuna(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Tuna";
            FoodValue = 8;
            CatchDifficulty = 0.5;
            BaseReproductionRate = 2;
            PollutionResilience = 0.5;
        }
    }

    public class Halibut : Fish
    {
        public Halibut(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Halibut";
            FoodValue = 4;
            CatchDifficulty = 0.5;
            BaseReproductionRate = 2;
            PollutionResilience = 0.5;
        }
    }

    public class Eel : Fish
    {
        public Eel(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Eel";
            FoodValue = 2;
            CatchDifficulty = 0.5;
            BaseReproductionRate = 0.25;
            PollutionResilience = 0.5;
        }
    }

    public class Garfish : Fish
    {
        public Garfish(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Garfish";
            FoodValue = 1.2;
            CatchDifficulty = 0.8;
            BaseReproductionRate = 0;
            PollutionResilience = 1;
            BycatchOnly = true;
        }
    }

    public class GiantOarfish : Fish
    {
        public GiantOarfish(uint initialPopulation) : base(initialPopulation)
        {
            Name = "Giant Oarfish";
            FoodValue = 10;
            CatchDifficulty = 0.99;
            BaseReproductionRate = 0;
            PollutionResilience = 1;
            BycatchOnly = true;
        }
    }
}