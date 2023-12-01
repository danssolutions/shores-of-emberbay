namespace TownOfZuul
{
    public class CommandWords
    {
        public List<string> ValidCommands { get; } = new List<string> { "north", "east", "south", "west", "look", "back", "quit", "exit", "help", "report", "info", "talk", "assign", "unassign", "clear", "boo", "sleep", "close", "xmas" };
        public bool IsValidCommand(string command)
        {
            return ValidCommands.Contains(command);
        }
    }

}
