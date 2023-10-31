using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownOfZuul
{
    public class CommandWords
    {
        public List<string> ValidCommands { get; } = new List<string> { "north", "east", "south", "west", "look", "back", "quit", "exit", "help", "info", "talk", "assign", "unassign", "clear" };

        public bool IsValidCommand(string command)
        {
            return ValidCommands.Contains(command);
        }
    }

}
