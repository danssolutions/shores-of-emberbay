namespace TownOfZuul
{
    public class ElderHouse : Location
    {
        public bool AlgaeCleanerUnlocked { get; private set; }
        public bool WaterFilterUnlocked { get; private set; }
        private readonly Elder elder = new();

        public ElderHouse()
        {
            Art = GameArt.ElderHouse;
            Name = "Village Elder's house";
            Description = "On the outskirts of town you find yourself looking at a small but well-maintained wooden shack." +
            " Although it is as old as most of the surrounding architecture," +
            " the passage of time has not managed to tear down this testament of the village's past greatness." +
            " You're in front of the village elder's house.\nIt looks like she is already waiting for you inside, eager to *talk* to you.";
            AlgaeCleanerUnlocked = false;
            WaterFilterUnlocked = false;
            Character = elder;
        }
    }
}
