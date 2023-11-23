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
                "Keep talking with the character",
                "How are you?",
                "Where's the village?",
                "Do you want my help?",
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
                    option1();
                /*
                    Console.Clear();
                    nextLine = "I'm good, idk how bout u.";
                    Console.WriteLine("idkkkkk");
                    Console.Clear();
                */
                    break;
                case 2:
                    option2();
                //    nextLine = "Thanks.";
                //    Console.Clear();
                    break;
                case 3:
                    option3();
                //    nextLine = "It's to the east, dummy. It's where you came from.";
                //    Console.Clear();
                    break;
                case 4:
                    option4();
                //    nextLine = "If you have accomplished cleaning the ocean, I can give you the algae cleaner";
                //    Console.Clear();
                    break;
                case 5:
                    option5();
                //    nextLine = "Good luck. I hope to see you soon.";

                    continueDisplay = false;
                    break;
            }
        }

        /*    override public void ParseEscapeOption()
            {
                Console.WriteLine("Good luck, bye.");
                continueDisplay = false;
            }
        */
            private static void option1()
            {
                Console.Clear();
                Console.CursorVisible = true;

                option1ElderMenu elderOption1 = new();
                elderOption1.Display();

                Console.Clear();

                //Console.WriteLine("you chose more");
 //               Console.WriteLine(currentLocation?.Description);
            }

            private static void option2()
            {
                Console.Clear();
                Console.CursorVisible = true;

                option2ElderMenu elderOption2 = new();
                elderOption2.Display();

                Console.Clear();

                Console.WriteLine("You chose option 2");
            }

            private static void option3()
            {
                Console.Clear();
                Console.CursorVisible = true;

                Console.Clear();

                Console.WriteLine("You chose option 3");
            }

            private static void option4()
            {
                Console.Clear();
                Console.CursorVisible = true;

                Console.Clear();                
                Console.WriteLine("You chose option 4");
            }

            private static void option5()
            {
                Console.Clear();
                Console.CursorVisible = true;

                Console.Clear();                
                Console.WriteLine("You chose option 5, Goodbye");
                
            }
        
        public sealed class option1ElderMenu : Character
        {
            private Location? currentLocation;
            
            private const string option1 = 
                "Thank you for for listening Mayor, Let me tell you about how everything changed for the worse " +
                "for everyone in the village. \nWhen I was just a child 60 years ago the village was thriving. "
                + "\nNow we are just trying to survive. Our health i getting worse for everyday, \nbecause we either don't "
                + "get anything to eat or because the fish we eat are contaminated with plastic or other chemicals. "
                + "\nCompanies take our fish so we barely have enough food and we have to be careful deciding what fish to catch. "
                + "\nThey pollute our water and take our fish. They are slowly moving away from our area, "
                + "\nbut now we need to think about what fish we catch and eat fish from polluted water. "
                + "\nWe need you, Mayor. Please help the village become sustainable and make it thrive again."
                + "\n"
                + "\nChoose what to answer the elder (type (1), for answer 1, (2) for answer 2):"
                + "\n- I will do what I can to help you and all the other villagers."
                + "\n- Times change, maybe it is okay like it is. We can just focus on other food sources.";

            private const string option11 = 
                "Thank you so much Mayor. If you succeed you will be our hero.";

            private const string option12 = 
                "We are dependent on the water and the food from the ocean. It makes up more than half of the villagers income and food source. "
                + "I don't believe that it would be the right decision to give up on this rich resource, that keeps us alive.";

            private const string option2 = 
                "That is okay. Maybe another time.";

            override public void Display()
            {
                Console.Clear();
                
                Console.WriteLine("Hello Mayor, thank you for visiting. You may know me as the Elder. " +
                "Do you want to hear the story about the good old days? " +
                "\n Type (y) for yes and (n) for no.");
                string readAnswer = Console.ReadLine();
                if (readAnswer != null)
                    if (readAnswer == "y")
                        Console.WriteLine(option1);
                            Console.WriteLine(option12);
                    else if (readAnswer == "n")
                        Console.WriteLine(option2);

                ConsoleKey key = Console.ReadKey(true).Key;
            }
        }

         public sealed class option2ElderMenu : Character
        {
            private Location? currentLocation;

            private const string option1 = 
                "Let us make sure you are happy";

            private const string option2 = 
                "No problem. I can be hard talking about";   

            override public void Display()
            {
                Console.Clear();
                
                Console.WriteLine("Should we talk about your feelings? " +
                "\n Type (y) for yes and (n) for no.");
                string readAnswer = Console.ReadLine();
                if (readAnswer != null)
                    if (readAnswer == "y")
                        Console.WriteLine(option1);
                    else
                        Console.WriteLine(option2);

                ConsoleKey key = Console.ReadKey(true).Key;
            }
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