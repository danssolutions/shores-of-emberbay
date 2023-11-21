namespace TownOfZuul
{
    public abstract class Character : Menu
    {
        //picture of each character
        public string? characterPicture { get; protected set; }

        //Dialogue menu
        //public menu? dialogueMenu;
        
        //Name of charater
        public string? characterName { get; protected set; }

        public string? nextLine { get; protected set; }

        //Unlock
        bool unlock = false;
        private const string more = "Keep talking with the character";
        private const string stop = "Stop if you have heard enough, and wish to move on";
        private const string who = "Learn more about how the character can help";

        //private const string items = "Ask what items you can unlock and how"; elder
        //private const string unlocked = "Unlock item"; elder

        public override void Display()
        {
            //Console.Clear();
            Console.WriteLine(Art);
            //Console.WriteLine(Text);

            while (continueDisplay)
            {
                Text = nextLine != Text ? nextLine : Text;
                Console.WriteLine("\r" + Text);

                for (int i = 1; i <= options.Length; i++)
                {
                    Console.Write((selectedOption == i ? ActiveOption : InactiveOption) + options[i - 1] + " (" + i + ")\n");
                }

                ConsoleKey key = Console.ReadKey(true).Key;

                Console.SetCursorPosition(0, Console.CursorTop - options.Length - 1);
                Console.CursorVisible = false;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedOption > 1)
                            selectedOption--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedOption < options.Length)
                            selectedOption++;
                        break;

                    case ConsoleKey.Enter:
                        ParseOption(selectedOption);
                        break;

                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                    case ConsoleKey.D6:
                    case ConsoleKey.D7:
                    case ConsoleKey.D8:
                    case ConsoleKey.D9:
                        ParseOption((int)key - 48);
                        break;

                    case ConsoleKey.Escape:
                        ParseEscapeOption();
                        break;
                }
            }
        }
    }

    public sealed class Elder : Character
    {
 //       string currentLocation = ElderHouse;
        /*string characterName = "Elder";
        string characterPicture = "Picture of elder";
        string more = "Thank you for for listening Mayor, Let me tell you about how everything changed for the worse " +
            "for everyone in the village. \nWhen I was just a child 60 years ago the village was thriving. "
            + "\nNow we are just trying to survive. Our health i getting worse for everyday, \nbecause we either don't "
            + "get anything to eat or because the fish we eat are contaminated with plastic or other chemicals. "
            + "\nCompanies take our fish so we barely have enough food and we have to be careful deciding what fish to catch. "
            + "\nThey pollute our water and take our fish. They are slowly moving away from our area, "
            + "\nbut now we need to think about what fish we catch and eat fish from polluted water. "
            + "\nWe need you, Mayor. Please help the village become sustainable and make it thrive again.";
        string stop = "No problem";
        string who = "You can unlock algae cleaner and water filter and get it from the Elder, when unlocked. "
            + "\nYou need to increase population health to more than 90 to unlock algae cleaner, then talk to the elder to get it.";
        */

        /*private const string Art =
            @"
             _____                            __   _____           _ 
            |_   _|____      ___ __     ___  / _| |__  /   _ _   _| |
              | |/ _ \ \ /\ / / '_ \   / _ \| |_    / / | | | | | | |
              | | (_) \ V  V /| | | | | (_) |  _|  / /| |_| | |_| | |
              |_|\___/ \_/\_/ |_| |_|  \___/|_|   /____\__,_|\__,_|_|
                                                                     
            ---------------------------------------------------------
                                                                     
            ";
*/
        //private string Text = "Well, hello there. Welcome to my humble abode.\n";

        public Elder()
        {
            Art =
            @"
             _____                            __   _____           _ 
            |_   _|____      ___ __     ___  / _| |__  /   _ _   _| |
              | |/ _ \ \ /\ / / '_ \   / _ \| |_    / / | | | | | | |
              | | (_) \ V  V /| | | | | (_) |  _|  / /| |_| | |_| | |
              |_|\___/ \_/\_/ |_| |_|  \___/|_|   /____\__,_|\__,_|_|
                                                                     
            ---------------------------------------------------------
                                                                     
            ";
            Text = "Well, hello there. Welcome to my humble abode.\n";
            characterName = "elder";

            options = new string[] {
                "How are you?",
                "I like your cut",
                "Where's the village?",
                "Goodbye"
            };
        }

        override public void Display()
        {
            Console.Clear();

            //Console.WriteLine(Art);
            //Console.WriteLine(Text);

            base.Display();

            Console.Clear();
        }

        override public void ParseOption(int option)
        {
            switch (option)
            {
                case 1:
                    Console.Clear();
                    nextLine = "I'm good, idk how bout u.";
                    Console.WriteLine("idkkkkk");
                    Console.Clear();
                    break;
                case 2:
                    nextLine = "Thanks.";
                    Console.Clear();
                    break;
                case 3:
                    nextLine = "It's to the east, dummy. It's where you came from.";
                    Console.Clear();
                    break;
                case 4:
                    nextLine = "Aight, bye.";

                    continueDisplay = false;
                    break;
            }
        }

        override public void ParseEscapeOption()
        {
            Console.WriteLine("Aight, bye.");
            continueDisplay = false;
        }
    }

    public sealed class Scientist : Character
    {
//        string currentLocation = wastePlant;
        /*string characterName = "Scientist";
        string characterPicture = "Picture of Scientist";
*/
        public Scientist()
        {
            characterName = "Scientist";
        }

    }

    public sealed class Npc : Character
    {

    }
}