namespace ShoresOfEmberbay
{
    public class CommandWords
    {
        public List<string> ValidCommands { get; } = new List<string> { 
            "north", 
            "east", 
            "south", 
            "west", 
            "look", 
            "back", 
            "quit", 
            "exit", 
            "help", 
            "report", 
            "info", 
            "talk", 
            "assign", 
            "unassign", 
            "clear", 
            "sleep", 
            "boo", 
            "xmas"
        };
        public bool IsValidCommand(string command)
        {
            return ValidCommands.Contains(command);
        }
    }

}
