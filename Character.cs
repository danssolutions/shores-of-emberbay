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
        public Elder()
        {

            Art = $@"
                      ___
                     (___)
                     / _ \
                    / / \ \
                    \/o o\/
                    (  /  )
                    `\_-_/`
                    / ___ \
                   / /_ _\ \
                   \ \___/ /
                    \/   \/
                    ||---||
                    ||---||
                    ||___||
                    |_____|
                      |||
                     /_|_\
            ";

            Text = "Well, hello there. Welcome to my humble abode.\n";
            characterName = "elder";

            options = new string[] {
                "Talk with the Elder",
                "Talk about feelings",
                "Get the directions to the village",
                "Ask the Elder how she can help",
                "quit"
            };
        }

        override public void Display()
        {
            Console.Clear();

            //Console.WriteLine(Art);
            Console.WriteLine("You have entered the elder's property. The wise elder can help you with what you need to know. "
            + "\nDon't be afraid to ask. Press (talk) to communicate with the elder.");

            base.Display();

            Console.Clear();
        }

        override public void ParseOption(int option)
        {
            switch (option)
            {
                case 1:
                    option1();
                    break;
                case 2:
                    option2();
                    break;
                case 3:
                    option3();
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
            }

            private static void option2()
            {
                Console.Clear();
                Console.CursorVisible = true;

                option2ElderMenu elderOption2 = new();
                elderOption2.Display();

                Console.Clear();
            }

            private static void option3()
            {
                Console.Clear();
                Console.CursorVisible = true;

                option3ElderMenu elderOption3 = new();
                elderOption3.Display();

                Console.Clear();
            }

            private static void option4()
            {
                Console.Clear();
                Console.CursorVisible = true;

                option4ElderMenu elderOption4 = new();
                elderOption4.Display();

                Console.Clear();
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
            private const string option1 = 
                "Thank you for for listening Mayor, Let me tell you about how everything changed for the worse " +
                "for everyone in the village. \nWhen I was just a child 60 years ago the village was thriving. "
                + "\nNow we are just trying to survive. Our health i getting worse for everyday, \nbecause we either don't "
                + "get anything to eat or because the fish we eat are contaminated with plastic or other chemicals. "
                + "\nCompanies take our fish so we barely have enough food and we have to be careful deciding what fish to catch. "
                + "\nThey pollute our water and take our fish. They are slowly moving away from our area, "
                + "\nbut now we need to think about what fish we catch and eat fish from polluted water. "
                + "\nWe need you, Mayor. Please help the village become sustainable and make it thrive again.";

            private const string option2 = 
                "That is okay. Maybe another time.";

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

            private static string[] optionYN = 
            {
                "(1) Yes",
                "(2) No"
            };

            private const string notValid = 
                "Please enter (1) for yes or (2) for no. Try again";

            override public void Display()
            {
                Console.Clear();
                
                Console.WriteLine("Hello Mayor, thank you for visiting. You may know me as the Elder. " +
                "Do you want to hear the story about the good old days?");

                Console.Clear();
                
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
                                if (readAnswer1 == "1")
                                    Console.WriteLine(option11[0]);
                                if (readAnswer1 == "2")
                                    Console.WriteLine(option11[1]);
                                else
                                    Console.WriteLine(notValid);
                            break;
                        case "2":
                            Console.WriteLine(option2);
                            break;
                    }
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

        public sealed class option3ElderMenu : Character
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

            private string[] optionYN = 
            {
                "(1) Yes",
                "(2) No"
            };

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
                            break;
                        case "2":
                            Console.WriteLine(optionN);
                            break;
                    }
                ConsoleKey key = Console.ReadKey(true).Key;
            }
        }

        public sealed class option4ElderMenu : Character
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

            private const string optionN = 
                "If you change your mind, just let me know.";

            private const string notValid = 
                "Please enter (1) for yes or (2) for no. Try again";

            private const string optionY =
                "By improving the health of the villagers you can expand the Village and get access to the Ocean. "
                + "To unlock the Wastewater Plant, you must have cleaned the ocean.";

            private string[] optionYN = 
            {
                "(1) Yes",
                "(2) No"
            };

            override public void Display()
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