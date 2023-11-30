namespace TownOfZuul
{
    public abstract class Character : Menu
    {
        public readonly string OriginalText;
        public string ReturnText;

        public Character()
        {
            OriginalText = ReturnText = Text ?? "";
        }

        public override void Display()
        {
            Console.Clear();
            Console.WriteLine(Art);
            Console.WriteLine(Text + "\n");

            while (continueDisplay)
            {
                for (int i = 1; i <= options.Length; i++)
                    Console.Write((selectedOption == i ? ActiveOption : InactiveOption) + options[i - 1] + " (" + i + ")\n");

                ConsoleKey key = Console.ReadKey(true).Key;

                Console.SetCursorPosition(0, Console.CursorTop - options.Length);
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
            continueDisplay = true;
        }
    }

    public class Elder : Character
    {
        public bool coastCleaned = false;
        public bool coastCleaningDiscussed = false;
        public bool nutrientsCleaned = false;
        public bool nutrientCleaningDiscussed = false;
        private const string BackText = "Hello again.";
        public Elder()
        {
            Art =
                $@"
  |   |   |   |   |   |   | ___|   |   |   |   |   |   |   |
  |   |   |   |   _________(___)   |   | ________________  |
  |   |   |   |  |  /   |  /~_~\   |   ||* * .... * *  * | |
  |   |   |   |  | / /  | /~/ \~\  |   || * . .. . *  *  | |
  |   |   |   |  |__/___|_\/0-0\/  |   ||*  * ..  *  *  *| |
  |   | ( |   |  |  /   | (  \  )  |   ||.....||.........| |
  |   |  )|   |  | /  / |  \_-_/|  |   ||________________| |
  |   _ ( ______ |___/__|_/|___|\  |   |   |   |   |   |   |
  |  / \_/>    /| |   |  / /   \ \ |   |   |   | O |   |   |
  | /_________/|| |   |  \ \___/ / |   |   |   O | O   |   |
  / ||/|| / || || /   /   \/   \/  /   /   /  /| | |  /   /
 /  || ||/  ||/||/   /   /|     | /   /   /  /_|_|_|_/   /
/   || ||   || ||   /   / |     |/   /   /  / \     /   /   
   /|| /   /|| /   /   /  |     |   /   /  /   \___/   /   /
------------------------------------------------------------
            ";

            Text = "Hi, Mayor, nice to finally meet you.";

            options = new string[]
            {
                "\"Can you tell me more about the village?.\"",
                "\"Can you help me with some directions?.\"",
                "\"How can you help me?.\"",
                "\"I wanted to ask if you have any tools available for ocean cleanup.\"",
                "\"Goodbye, see you soon.\""
            };
        }

        public override void ParseOption(int option)
        {
            switch (option)
            {
                case 1:
                    Console.Clear();
                    TextChangeMenu textChangeMenu = new();
                    textChangeMenu.Display();
                    Text = textChangeMenu.ReturnText;
                    Console.Clear();
                    Console.WriteLine(Art);
                    Console.WriteLine(Text);
                    break;
                case 2:
                    Console.Clear();
                    ReturnTextChangeMenu returnTextChangeMenu = new();
                    returnTextChangeMenu.Display();
                    Text = returnTextChangeMenu.ReturnText;
                    Console.Clear();
                    Console.WriteLine(Art);
                    Console.WriteLine(Text);
                    break;
                case 3:
                    Console.Clear();
                    ReturnTextChangeMenu2 returnTextChangeMenu2 = new();
                    returnTextChangeMenu2.Display();
                    Text = returnTextChangeMenu2.ReturnText;
                    Console.Clear();
                    Console.WriteLine(Art);
                    Console.WriteLine(Text);
                    break;
                case 4:
                    Console.Clear();
                    // add option check here
                    // if unlocked, display one type of menu and set __CleaningDiscussed to true
                    // if still locked, display a different type of menu informing player that they still need to clean a specific location
                    Console.Clear();
                    Console.WriteLine(Art);
                    Console.WriteLine(Text);
                    break;
                case 5:
                    ParseEscapeOption();
                    break;
            }
        }

        override public void ParseEscapeOption()
        {
            continueDisplay = false;
            Text = BackText;
            Console.CursorVisible = true;
        }

        public sealed class TextChangeMenu : Character
        {
            public TextChangeMenu()
            {
                Art =
                    $@"
  |   |   |   |   |   |   | ___|   |   |   |   |   |   |   |
  |   |   |   |   _________(___)   |   | ________________  |
  |   |   |   |  |  /   |  /~_~\   |   ||* * .... * *  * | |
  |   |   |   |  | / /  | /~/ \~\  |   || * . .. . *  *  | |
  |   |   |   |  |__/___|_\/0-0\/  |   ||*  * ..  *  *  *| |
  |   | ( |   |  |  /   | (  \  )  |   ||.....||.........| |
  |   |  )|   |  | /  / |  \_-_/|  |   ||________________| |
  |   _ ( ______ |___/__|_/|___|\  |   |   |   |   |   |   |
  |  / \_/>    /| |   |  / /   \ \ |   |   |   | O |   |   |
  | /_________/|| |   |  \ \___/ / |   |   |   O | O   |   |
  / ||/|| / || || /   /   \/   \/  /   /   /  /| | |  /   /
 /  || ||/  ||/||/   /   /|     | /   /   /  /_|_|_|_/   /
/   || ||   || ||   /   / |     |/   /   /  / \     /   /   
   /|| /   /|| /   /   /  |     |   /   /  /   \___/   /   /
------------------------------------------------------------
                ";

                Text = "When I was just a child 60 years ago the village was thriving. " +
                "\nNow we are just trying to survive. Our health i getting worse for everyday, because we either don't get anything to eat " +
                "\nor because the fish we eat are contaminated with plastic or other chemicals. " +
                "\nCompanies took our fish so we barely have enough food and we have to be careful deciding what fish to catch. " +
                "\nThey polluted our water. They are slowly moving away from our area, " +
                "\nbut now we need to think about what fish we catch and eat fish from polluted water.";

                options = new string[]
                {
                    "\"I will do everyting in my power to help the village. Do you have some advise?\"",
                    "\"Maybe we should focus on other resources instead of resources from the water?\"",
                    "\"Thank you for sharing your story. You should have been the Mayor of the village\"",
                    "\"Do you think it is too late to change things for the better?\"",
                    "\"To be honest I do not think there is any problems with how the village is now.\"",
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "I advise you to take into consideration what you fish and to treat bodies of water with respect \n"
                        + "You can also talk with the scientist, I am sure he has some valuable insight.";
                        break;
                    case 2:
                        ReturnText = "We are dependent on the water and the food from the ocean. " +
                    "It makes up more than half of the villagers income and food source. " +
                    "\nI don't believe that it would be the right decision to give up on this rich resource, that keeps us alive. " +
                    "\nI hope you will change your mind.\n";
                        break;
                    case 3:
                        ReturnText = "I believe that you will prove to be the Mayor this village needs.\n";
                        break;
                    case 4:
                        ReturnText = "I believe that it is still possible, but only with imidiate action.\n";
                        break;
                    case 5:
                        ReturnText = "I hope after looking around, you will change you mind.\n";
                        break;
                }
                continueDisplay = false;
            }
        }

        public sealed class ReturnTextChangeMenu : Character
        {
            
            public ReturnTextChangeMenu()
            {
                Art =
                    $@"
  |   |   |   |   |   |   | ___|   |   |   |   |   |   |   |
  |   |   |   |   _________(___)   |   | ________________  |
  |   |   |   |  |  /   |  /~_~\   |   ||* * .... * *  * | |
  |   |   |   |  | / /  | /~/ \~\  |   || * . .. . *  *  | |
  |   |   |   |  |__/___|_\/0-0\/  |   ||*  * ..  *  *  *| |
  |   | ( |   |  |  /   | (  \  )  |   ||.....||.........| |
  |   |  )|   |  | /  / |  \_-_/|  |   ||________________| |
  |   _ ( ______ |___/__|_/|___|\  |   |   |   |   |   |   |
  |  / \_/>    /| |   |  / /   \ \ |   |   |   | O |   |   |
  | /_________/|| |   |  \ \___/ / |   |   |   O | O   |   |
  / ||/|| / || || /   /   \/   \/  /   /   /  /| | |  /   /
 /  || ||/  ||/||/   /   /|     | /   /   /  /_|_|_|_/   /
/   || ||   || ||   /   / |     |/   /   /  / \     /   /   
   /|| /   /|| /   /   /  |     |   /   /  /   \___/   /   /
------------------------------------------------------------
                ";

                Text = "Ofcourse, what do you need the direction to?";

                options = new string[]
                {
                    "\"Where is the Docks?\"",
                    "\"Where is the Ocean?\"",
                    "\"Where is the researchVessel?\"",
                    "\"Where is the Wastewater Treatment plant?\"",
                    "\"Where is the Coast?\"",
                    "\"Where is the Village?\""
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "The Docks is to the east and then more east.\n";
                        continueDisplay = false;
                        break;
                    case 2:
                        ReturnText = "The Ocean is as far to the east as you can get in this village.\n";
                        continueDisplay = false;
                        break;
                    case 3:
                        ReturnText = "The researchVessel is east and then more east and then go to the north.\n";
                        continueDisplay = false;
                        break;
                    case 4:
                        ReturnText = "The Wastewater Treatment Plant is east and then as far south as you can go.\n";
                        continueDisplay = false;
                        break;
                    case 5:
                        ReturnText = "The Coast is east, then south.\n";
                        continueDisplay = false;
                        break;
                    case 6:
                        ReturnText = "The Village is to the east. You came here from the village, so just go back the way you came from\n";
                        continueDisplay = false;
                        break;
                }
            }
        }

        public sealed class ReturnTextChangeMenu2 : Character
        {
            public ReturnTextChangeMenu2()
            {
                Art =
                    $@"
  |   |   |   |   |   |   | ___|   |   |   |   |   |   |   |
  |   |   |   |   _________(___)   |   | ________________  |
  |   |   |   |  |  /   |  /~_~\   |   ||* * .... * *  * | |
  |   |   |   |  | / /  | /~/ \~\  |   || * . .. . *  *  | |
  |   |   |   |  |__/___|_\/0-0\/  |   ||*  * ..  *  *  *| |
  |   | ( |   |  |  /   | (  \  )  |   ||.....||.........| |
  |   |  )|   |  | /  / |  \_-_/|  |   ||________________| |
  |   _ ( ______ |___/__|_/|___|\  |   |   |   |   |   |   |
  |  / \_/>    /| |   |  / /   \ \ |   |   |   | O |   |   |
  | /_________/|| |   |  \ \___/ / |   |   |   O | O   |   |
  / ||/|| / || || /   /   \/   \/  /   /   /  /| | |  /   /
 /  || ||/  ||/||/   /   /|     | /   /   /  /_|_|_|_/   /
/   || ||   || ||   /   / |     |/   /   /  / \     /   /   
   /|| /   /|| /   /   /  |     |   /   /  /   \___/   /   /
------------------------------------------------------------
                ";

                Text = "I am delighted to know that someone appreciate the knowledge of en elder. "
                + "How can I help?";

                options = new string[]
                {
                    "\"What places would you recommend me to visit in the Village?\"",
                    "\"Would you be able to help me find some usefull tools?\"",
                    "\"How can I improve the Village?\"",
                    "\"Whom can I ask for help?\""
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "I would recommend visiting the Docks and the WasteWater Plant, and when possible the Ocean.";
                        break;
                    case 2:
                        ReturnText = "Yes, I have a few tools for ocean cleanup in mind that I'm working on getting for you.\n" +
                        "In the meantime, focus on what you can do right now. For example, maybe you can clean the Coast from trash.\n";
                        break;
                    case 3:
                        ReturnText = "To improve a Village you must improve the lives of the people in the Village. "
                    + "\nThe overall health is decreasing. People are getting sick. The waters needs to be cleaned, "
                    + "\nfor the Villagers to be able to drink, eat and work, thereby becomming happy and healthy.\n";
                        break;
                    case 4:
                        ReturnText = "I am always here to help, "
                    + "\nbut the intelligent scientist at the Research vessel might also be a great help.\n";
                        break;
                }
                continueDisplay = false;
            }
        }
    }

   /* public sealed class Elder : Character
    {
        public bool nutrientCleaningUnlocked = false;
        public bool wastePlantUnlocked = false;
        public Elder()
        {
            Art =
 @"
  |   |   |   |   |   |   | ___|   |   |   |   |   |   |   |
  |   |   |   |   _________(___)   |   | ________________  |
  |   |   |   |  |  /   |  /~_~\   |   ||* * .... * *  * | |
  |   |   |   |  | / /  | /~/ \~\  |   || * . .. . *  *  | |
  |   |   |   |  |__/___|_\/0-0\/  |   ||*  * ..  *  *  *| |
  |   | ( |   |  |  /   | (  \  )  |   ||.....||.........| |
  |   |  )|   |  | /  / |  \_-_/|  |   ||________________| |
  |   _ ( ______ |___/__|_/|___|\  |   |   |   |   |   |   |
  |  / \_/>    /| |   |  / /   \ \ |   |   |   | O |   |   |
  | /_________/|| |   |  \ \___/ / |   |   |   O | O   |   |
  / ||/|| / || || /   /   \/   \/  /   /   /  /| | |  /   /
 /  || ||/  ||/||/   /   /|     | /   /   /  /_|_|_|_/   /
/   || ||   || ||   /   / |     |/   /   /  / \     /   /   
   /|| /   /|| /   /   /  |     |   /   /  /   \___/   /   /
------------------------------------------------------------
            ";

            Text = "Well, hello there. Welcome to my humble abode.\n";

            options = new string[]
            {
                "\"How is it going?\"",
                "\"Should we talk about our feelings?\"",
                "\"Can you give me the directions to the village?\"",
                "\"How can you help?\"",
                "\"Goodbye, see you soon.\""
            };
        }

        public override void Display()
        {
            Console.Clear();

            base.Display();
        }

        public override void ParseOption(int option)
        {
            switch (option)
            {
                case 1:
                    TalkOption();
                    break;
                case 2:
                    FeelingOption();
                    break;
                case 3:
                    DirectionsOption();
                    break;
                case 4:
                    HelpOption();
                    //    nextLine = "If you have accomplished cleaning the ocean, I can give you the algae cleaner";
                    break;
                case 5:
                    continueDisplay = false;
                    break;
            }
        }

            override public void ParseEscapeOption()
            {
                Console.WriteLine("Good luck, bye.");
                continueDisplay = false;
            }
        
        private void TalkOption()
        {
            Console.Clear();
            //Console.CursorVisible = true;


            TalkMenu elderOption1 = new();
            elderOption1.Display();
            Text = TalkMenu.TalkBack;

            Console.Clear();
            Console.WriteLine(Art);
            Console.WriteLine(Text);
        }

        private static void FeelingOption()
        {
            Console.Clear();
            Console.CursorVisible = true;

            FeelingsMenu elderOption2 = new();
            elderOption2.Display();

            Console.Clear();
        }

        private static void DirectionsOption()
        {
            Console.Clear();
            Console.CursorVisible = true;

            DirectionsMenu elderOption3 = new();
            elderOption3.Display();

            Console.Clear();
        }

        private static void HelpOption()
        {
            Console.Clear();
            Console.CursorVisible = true;

            HelpMenu elderOption4 = new();
            elderOption4.Display();

            Console.Clear();
        }

        public sealed class TalkMenu : Character
        {
            //Constructor
            /*public void TalkMenu()
            {
                Text = 
            }

            public const string TalkBack = "Ok, what else do you need to ask?";
            private const string option1 =
                "When I was just a child 60 years ago the village was thriving. " +
                "\nNow we are just trying to survive. Our health i getting worse for everyday, " +
                "\nbecause we either don't get anything to eat " +
                "\nor because the fish we eat are contaminated with plastic or other chemicals. " +
                "\nCompanies take our fish so we barely have enough food and we have to be careful deciding what fish to catch. " +
                "\nThey pollute our water and take our fish. They are slowly moving away from our area, " +
                "\nbut now we need to think about what fish we catch and eat fish from polluted water.";

            private const string option2 = "That is okay. Maybe another time.";

            private string[] option10 =
            {
                "(1) I will do everyting in my power to help the village.",
                "(2) Maybe we should focus on other resources instead of what is in the water."
            };

            private static string[] option11 =
            {
                "Thank you so much Mayor. If you succeed you will be our hero.",
                "We are dependent on the water and the food from the ocean. "
                    + "It makes up more than half of the villagers income and food source. "
                    + "\nI don't believe that it would be the right decision to give up on this rich resource, that keeps us alive. "
                    + "\nI hope you will change your mind."
            };

            private static string[] optionYN = { "(1) Yes", "(2) No" };

            private const string notValid = "Please enter (1) for yes or (2) for no. Try again";

            public override void Display()
            {
                Console.Clear();

                Console.WriteLine(
                    "Hello Mayor, thank you for visiting. You may know me as the Elder. "
                        + "Do you want to hear the story about the good old days?"
                );

                Console.WriteLine(optionYN[0]);
                Console.WriteLine(optionYN[1]);

                string readAnswer = Console.ReadLine();
                Console.Clear();
                if (readAnswer != null)
                    switch (readAnswer)
                    {
                        case "1":
                            Console.WriteLine(option1);
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine(option10[0]);
                            Console.WriteLine(option10[1]);
                            string readAnswer1 = Console.ReadLine();
                            Console.Clear();
                            if (readAnswer1 != null)
                            {
                                if (readAnswer1 == "1")
                                    Console.WriteLine(option11[0]);
                                if (readAnswer1 == "2")
                                    Console.WriteLine(option11[1]);
                                else
                                    Console.WriteLine(notValid);
                            }
                            break;
                        case "2":
                            Console.WriteLine(option2);
                            break;
                    }
                ConsoleKey key = Console.ReadKey(true).Key;
            }
        }

        public sealed class FeelingsMenu : Character
        {
            private Location? currentLocation;

            private const string option1 = "Let us make sure you are happy";

            private const string option2 = "No problem. I can be hard talking about";

            public override void Display()
            {
                Console.Clear();

                Console.WriteLine(
                    "Should we talk about your feelings? " + "\n Type (y) for yes and (n) for no."
                );
                string readAnswer = Console.ReadLine();
                if (readAnswer != null)
                    if (readAnswer == "y")
                        Console.WriteLine(option1);
                    else
                        Console.WriteLine(option2);

                ConsoleKey key = Console.ReadKey(true).Key;
            }
        }

        public sealed class DirectionsMenu : Character
        {
            private const string option1 =
                "The Village is the way you came from, (east). "
                + "Do yo want to know how to get to the other places from here?";

            private string[] optionY =
            {
                "(1) Where is the Docks?",
                "(2) Where is the Ocean?",
                "(3) Where is the researchVessel?",
                "(4) Where is the Wastewater Treatment plant?",
                "(5) Where is the Coast?",
                "(6) Where is the Village?"
            };

            private string[] option11 =
            {
                "The Docks is east and then more east ",
                "The Ocean is (direction) (state: locked/unlocked) ",
                "The researchVessel is east and then more east and then go to the north",
                "The Wastewater Treatment Plant is east, south and more south (state: locked/unlocked)",
                "The Coast is east, south ",
                "The Village is to the east."
            };

            private const string optionN =
                "You can always ask me if you forget, as long as you remember the way to my house.";

            private string[] optionYN = { "(1) Yes", "(2) No" };

            //private const string notValid =
            //    "Please enter a number between 1 and 6.";

            override public void Display()
            {
                Console.Clear();

                Console.WriteLine(option1);
                Console.ReadKey();
                Console.Clear();

                Console.WriteLine(optionYN[0]);
                Console.WriteLine(optionYN[1]);

                string readAnswer = Console.ReadLine();
                Console.Clear();
                if (readAnswer != null)
                    switch (readAnswer)
                    {
                        case "1":
                            Console.WriteLine(optionY[0]);
                            Console.WriteLine(optionY[1]);
                            Console.WriteLine(optionY[2]);
                            Console.WriteLine(optionY[3]);
                            Console.WriteLine(optionY[4]);
                            Console.WriteLine(optionY[5]);
                            string readAnswer1 = Console.ReadLine();
                            Console.Clear();
                            if (readAnswer1 != null)
                            {
                                if (readAnswer1 == "1")
                                    Console.WriteLine(option11[0]);
                                if (readAnswer1 == "2")
                                    Console.WriteLine(option11[1]);
                                if (readAnswer1 == "3")
                                    Console.WriteLine(option11[2]);
                                if (readAnswer1 == "4")
                                    Console.WriteLine(option11[3]);
                                if (readAnswer1 == "5")
                                    Console.WriteLine(option11[4]);
                                if (readAnswer1 == "6")
                                    Console.WriteLine(option11[5]);
                            }
                            break;
                        case "2":
                            Console.WriteLine(optionN);
                            break;
                    }
                ConsoleKey key = Console.ReadKey(true).Key;
            }
        }

        public sealed class HelpMenu : Character
        {
            private const string option1 =
                "Hello Mayor, I am delighted to know that someone appreciate the knowledge of en elder. "
                + "How can I help?";

            private string[] option11 =
            {
                "(1) What areas can be unlocked?",
                "(2) What items can I unlock?",
                "(3) How can I improve the Village?",
                "(4) Whom can I ask for help?"
            };

            private string[] option111 =
            {
                "The Ocean and the Wastewater Plant can be unlocked. "
                    + "\nDo you want to know more about how you can unlock these areas?",
                "If you clean the Coast from trash, I can help you clean the water even better. "
                    + "\nCome back to me when you have come to that and you will receive a gift.",
                "To improve a Village you must improve the lives of the people in the Village. "
                    + "\nThe overall health is decreasing. People are getting sick. The waters needs to be cleaned, "
                    + "\nfor the Villagers to be able to drink, eat and work, thereby becomming happy and healthy.",
                "I am always here to help, "
                    + "\nbut the intelligent scientist at the Research vessel might also be a great help.",
            };

            private const string optionN = "If you change your mind, just let me know.";

            private const string notValid = "Please enter (1) for yes or (2) for no. Try again";

            private const string optionY =
                "By improving the health of the villagers you can expand the Village and get access to the Ocean. "
                + "To unlock the Wastewater Plant, you must have cleaned the ocean.";

            private string[] optionYN = { "(1) Yes", "(2) No" };

            public override void Display()
            {
                Console.Clear();

                Console.WriteLine(option1);
                Console.ReadKey();
                Console.Clear();

                Console.WriteLine(option11[0]);
                Console.WriteLine(option11[1]);
                Console.WriteLine(option11[2]);
                Console.WriteLine(option11[3]);

                string readAnswer = Console.ReadLine();
                Console.Clear();
                if (readAnswer != null)
                    switch (readAnswer)
                    {
                        case "1":
                            Console.WriteLine(option111[0]);
                            Console.ReadKey();
                            Console.Clear();

                            Console.WriteLine(optionYN[0]);
                            Console.WriteLine(optionYN[1]);

                            string readAnswer1 = Console.ReadLine();
                            Console.Clear();
                            if (readAnswer1 != null)
                                if (readAnswer1 == "1")
                                    Console.WriteLine(optionY);
                            if (readAnswer1 == "2")
                                Console.WriteLine(optionN);
                            else
                                Console.WriteLine(notValid);
                            break;
                        case "2":
                            Console.WriteLine(option111[1]);
                            break;
                        case "3":
                            Console.WriteLine(option111[2]);
                            break;
                        case "4":
                            Console.WriteLine(option111[3]);
                            break;
                    }
                ConsoleKey key = Console.ReadKey(true).Key;
            }
        }
    }
*/
    public sealed class Scientist : Character
    {
        //        string currentLocation = wastePlant;
        string characterName = "Scientist";
        string characterPicture = "Picture of Scientist";

        public Scientist()
        {
        }
    }

    public sealed class Npc : Character { }
}
