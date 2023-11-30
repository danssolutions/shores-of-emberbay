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
                    ReturnTextChangeMenu3 returnTextChangeMenu3 = new();
                    ReturnTextChangeMenu4 returnTextChangeMenu4 = new();

                    if (coastCleaned == false)
                    {
                        returnTextChangeMenu3.Display();  
                        Text = returnTextChangeMenu3.ReturnText;
                        Console.Clear();
                        Console.WriteLine(Art);
                        Console.WriteLine(Text);
                    }
                    else if (coastCleaned == true)
                    {
                        nutrientsCleaned = true;
                        returnTextChangeMenu4.Display();
                        Text = returnTextChangeMenu4.ReturnText;
                        Console.Clear();
                        Console.WriteLine(Art);
                        Console.WriteLine(Text);
                    }
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
                        ReturnText = "I would recommend visiting the Docks and the WasteWater Plant, and when possible the Ocean.\n";
                        continueDisplay = false;
                        break;
                    case 2:
                        ReturnText = "Yes, I have a few tools for ocean cleanup in mind that I'm working on getting for you.\n" +
                        "In the meantime, focus on what you can do right now. For example, maybe you can clean the Coast from trash.\n";
                        continueDisplay = false;
                        break;
                    case 3:
                        ReturnText = "To improve a Village you must improve the lives of the people in the Village. "
                    + "\nThe overall health is decreasing. People are getting sick. The waters needs to be cleaned, "
                    + "\nfor the Villagers to be able to drink, eat and work, thereby becomming happy and healthy.\n";
                        continueDisplay = false;
                        break;
                    case 4:
                        ReturnText = "I am always here to help, "
                    + "\nbut the intelligent scientist at the Research vessel might also be a great help.\n";
                        continueDisplay = false;
                        break;
                }
            }
        }

        public sealed class ReturnTextChangeMenu3 : Character
        {
            
            public ReturnTextChangeMenu3()
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

                Text = "What task do you need these tools for? Then I will check if I have them.";

                options = new string[]
                {
                    "\"I was wondering if you have some tools for cleening the garbage more effeciently.\"",
                    "\"I could really use some tools to clean the nutrients from the water\""
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "I believe you have the best available tools in this Village for that task.\n";
                        continueDisplay = false;
                        break;
                    case 2:
                        ReturnText = "I believe I have something for that, but I need some parts for it to be fully functioning first. \n" +
                        "If you come back later, it might be ready for you. In the meantime maybe you can work on cleaning the garbage from the coast.\n";
                        continueDisplay = false;
                        break;
                }
            }
        }

    public sealed class ReturnTextChangeMenu4 : Character
        {
            
            public ReturnTextChangeMenu4()
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

                Text = "Are you thinking if the tool is ready for cleaning the nutrients?";

                options = new string[]
                {
                    "\"Yes, exactly. Now would be a great time to upgrade to more advanced tools to clean the water.\"",
                    "\"I was actually thinking if you might have found other helpfull tools.\""
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "Perfect timing, I just got the tool up and running. Here you go... I hope it will be usefull.\n";
                        continueDisplay = false;
                        break;
                    case 2:
                        ReturnText = "I have not found any other tools, but I will to give you the tool for cleaning nurtrients. \n";
                        continueDisplay = false;
                        break;
                }
                continueDisplay = false;
            }
        }
    }

    

    public class Fisherman : Character
    {
        private const string BackText = "Ahoy, mayor. Fair winds.";
        public Fisherman()
        {
            Art =
          @"
                             _ 
                  V        _/=\_               ,^._______,^.
  V                      _/=====\_            .|
                           '-\-'           ,-`|-|   |-|
                           ' ,=~U      ,-~'  _|_|___|_|_
            V             __)-(___  ,-`     /IIIIIIIIIII\
                         /  <\/>  \'    ____||_________||___
------------------------/ /|  : |\ \_.  |[][][][][ ][][][][]
         -      -   _  / / |  : | \ \ `-|___________________
  ~            ~    \\/_/  }===={  \)() /__/____/____/____/_
      ~     -        (('0  |)  (|   || _______________ _____
   -          -     -  \\  |    |   ||/____________(___|||||
        ~   ,___________`\ |  | |___||_________________|||||
~          /_-______=_____\|  | |___||___-______;______|()||
------------------------------------------------------------
            ";

            Text = "Ahoy. You must be the new mayor. Maybe things will finally turn around for us ol' anglers...";

            options = new string[]
            {
                "\"You look like you've been a local for a long time here.\"",
                "\"Any tips or suggestions you could give me?\"",
                "\"Have there been any complaints from the locals in regards to anything?\"",
                "\"How would one get to the ocean in the east?\"",
                "\"I must go now. Good luck with the fishing!\""
            };
        }

        public override void ParseOption(int option)
        {
            switch (option)
            {
                case 1:
                    Console.Clear();
                    StoryMenu storyMenu = new();
                    storyMenu.Display();
                    Text = storyMenu.ReturnText;
                    Console.Clear();
                    Console.WriteLine(Art);
                    Console.WriteLine(Text);
                    break;
                case 2:
                    Console.Clear();
                    HelpMenu helpMenu = new();
                    helpMenu.Display();
                    Text = helpMenu.ReturnText;
                    Console.Clear();
                    Console.WriteLine(Art);
                    Console.WriteLine(Text);
                    break;
                case 3:
                    Console.Clear();
                    PollutionMenu pollutionMenu = new();
                    pollutionMenu.Display();
                    Text = pollutionMenu.ReturnText;
                    Console.Clear();
                    Console.WriteLine(Art);
                    Console.WriteLine(Text);
                    break;
                case 4:
                    Console.Clear();
                    OceanAccessMenu oceanAccessMenu = new();
                    oceanAccessMenu.Display();
                    Text = oceanAccessMenu.ReturnText;
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

        public sealed class StoryMenu : Character
        {
            public StoryMenu()
            {
                Art =
          @"
                             _ 
                  V        _/=\_               ,^._______,^.
  V                      _/=====\_            .|
                           '-\-'           ,-`|-|   |-|
                           ' ,=~U      ,-~'  _|_|___|_|_
            V             __)-(___  ,-`     /IIIIIIIIIII\
                         /  <\/>  \'    ____||_________||___
------------------------/ /|  : |\ \_.  |[][][][][ ][][][][]
         -      -   _  / / |  : | \ \ `-|___________________
  ~            ~    \\/_/  }===={  \)() /__/____/____/____/_
      ~     -        (('0  |)  (|   || _______________ _____
   -          -     -  \\  |    |   ||/____________(___|||||
        ~   ,___________`\ |  | |___||_________________|||||
~          /_-______=_____\|  | |___||___-______;______|()||
------------------------------------------------------------
                ";

                Text = "Hah, indeed I have. Been fishing and living in Emberbay with my family for... 40 years? 50, maybe? " + 
                "Used to know the old mayor personally, back when this place was in its prime.\n" +
                "Y'know, it's a shame, what happened to this ol' town. You should've seen it before all these factories were built, " +
                "it was as beautiful as the seas themselves.";

                options = new string[]
                {
                    "\"Sounds like you're well acquainted. Have you met the village elder?\"",
                    "\"You mentioned factories. Did the village deteriorate because of them?\"",
                    "\"I spotted a research vessel here in the harbor. Has it been here for long?\"",
                    "\"What's with that old sewage plant in the far south?\"",
                    "\"Yeah, I believe you. I wanted to ask something else.\"",
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "Much obliged. The elder's been here longer than I have, believe it or not. " +
                        "Lives all the way west from here, if you haven't met her yet.\n" +
                        "Don't tell her I said it, but she's the old mayor I was talking about, though she doesn't like talking about it. " + 
                        "Retired a while ago, something regarding health issues, \n" +
                        "but I'm pretty sure she'd jump at the opportunity to help you out, heh.\n";
                        break;
                    case 2:
                        ReturnText = "Aye, of course it did. Bunch o' industrious types came in, built canneries, fishpacking plants, all kinds of eyesores. " +
                        "And on top of that, they plundered the seas and dumped all kinds of waste to the local waters for good measure.\n " +
                        "When they reeled all the fish out the water, they scuttled and left their factories behind to rot, " +
                        "and the local fish haven't recovered ever since.\n";
                        break;
                    case 3:
                        ReturnText = "Nay, that ship's recent. She first anchored here less than a couple years ago. Don't know what that crew's up to, " + 
                        "not like there's good trawlin' to be made here.\n";
                        break;
                    case 4:
                        ReturnText = "Oh, that thing? That used to be the ol' wastewater plant back when the factories in Emberbay were still running.\n" +
                        "People realized back then that we were polluting the seas too much and built it to mitigate the damage, but by that point " +
                        "it was too late. When the factories closed, anyone that could pay the plant workers well enough left the town high and dry, " +
                        "and it became dead in the water. Yep.\n";
                        break;
                    case 5:
                        ReturnText = "Go ahead.\n";
                        break;
                }
                continueDisplay = false;
            }
        }

        public sealed class HelpMenu : Character
        {
            public HelpMenu()
            {
                Art =
          @"
                             _ 
                  V        _/=\_               ,^._______,^.
  V                      _/=====\_            .|
                           '-\-'           ,-`|-|   |-|
                           ' ,=~U      ,-~'  _|_|___|_|_
            V             __)-(___  ,-`     /IIIIIIIIIII\
                         /  <\/>  \'    ____||_________||___
------------------------/ /|  : |\ \_.  |[][][][][ ][][][][]
         -      -   _  / / |  : | \ \ `-|___________________
  ~            ~    \\/_/  }===={  \)() /__/____/____/____/_
      ~     -        (('0  |)  (|   || _______________ _____
   -          -     -  \\  |    |   ||/____________(___|||||
        ~   ,___________`\ |  | |___||_________________|||||
~          /_-______=_____\|  | |___||___-______;______|()||
------------------------------------------------------------
                ";

                Text = "Tips? Aye, can do. People here need food, and to get food, they need us anglers. " +
                "The locals are eager to get to casting right now, but they're cautious. " + 
                "Some of the local fish here have almost gone extinct as a result of someone getting a little too greedy plundering the seas.\n" +
                "If you're looking to assign folk to cast for some fish here, let me know and I'll help you arrange it.";

                options = new string[]
                {
                    "\"How would I let the locals know what type of fish they should catch?\"",
                    "\"How much would a single fisher be able to catch every month?\"",
                    "\"I'm guessing that a fisher assigned to catch one type of fish may unintentionally catch another?\"",
                    "\"Good to know.\"",
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "Whenever you're here at the docks, let me know that you want to *assign* a number of people to fish here.\n" +
                        "I'll prepare a list with all the local fish types, as well as how difficult each one is to catch, and you can get certain " +
                        "people to cast for specific fish.\n" +
                        "When you're done, I'll add your plans to the bulletin board and let everyone know what to do.\n";
                        break;
                    case 2:
                        ReturnText = "Depends on the fish. Some types are more difficult to catch while others are easier.\n" +
                        "A single fisher can pull about a hundred fish per month. The catch rate can get higher, but I've never seen anyone get more " +
                        "than a couple hundred fish in a single month. Bycatch can also bump the catch rate up to some degree.\n";
                        break;
                    case 3:
                        ReturnText = "Aye, that's what we call bycatch. Usually, the locals practice catch and release for fish they weren't trying to go for, " + 
                        "but sometimes the fish that strike are dying anyway, so you might as well throw it into the crock pot, hehe.\n";
                        break;
                    case 4:
                        ReturnText = "Anything else?\n";
                        break;
                }
                continueDisplay = false;
            }
        }

        public sealed class PollutionMenu : Character
        {
            public PollutionMenu()
            {
                Art =
          @"
                             _ 
                  V        _/=\_               ,^._______,^.
  V                      _/=====\_            .|
                           '-\-'           ,-`|-|   |-|
                           ' ,=~U      ,-~'  _|_|___|_|_
            V             __)-(___  ,-`     /IIIIIIIIIII\
                         /  <\/>  \'    ____||_________||___
------------------------/ /|  : |\ \_.  |[][][][][ ][][][][]
         -      -   _  / / |  : | \ \ `-|___________________
  ~            ~    \\/_/  }===={  \)() /__/____/____/____/_
      ~     -        (('0  |)  (|   || _______________ _____
   -          -     -  \\  |    |   ||/____________(___|||||
        ~   ,___________`\ |  | |___||_________________|||||
~          /_-______=_____\|  | |___||___-______;______|()||
------------------------------------------------------------
                ";

                Text = "Hah, aye, just about everybody can't shut up about the water quality. " +
                "The pollution here's been horrible for as long as the factories have been closed. It hurts the fish as well as the locals. " + 
                "Seen too many people become sick from the water here... Hey, maybe you could do something about it?";

                options = new string[]
                {
                    "\"What kind of pollution?\"",
                    "\"The bad water is damaging to the fish as well?\"",
                    "\"How could I help with the water quality situation?\"",
                    "\"I'll keep this in mind.\"",
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "There's piles of garbage floating around the waters, for a start. There's also that waste the old factories " +
                        "were dumping into the seas. I don't know what it was, but it's causing the water to turn green, some kind of algae spreading " +
                        "everywhere. I wouldn't be surprised if there was something else nasty that I wouldn't even know about.\n";
                        break;
                    case 2:
                        ReturnText = "Aye. If not for the pollution, I wouldn't be surprised if the local fish stock fully recovered by now.\n";
                        break;
                    case 3:
                        ReturnText = "Hmm... you could try assigning some people to clean up the garbage in the seas. There's a spot in the " +
                        "coast south of the village that might be a good spot to start in. I'm not sure how to deal with any of the other problems " +
                        "in the water though. You might want to ask the village elder for help on that one. She lives far to the west of here.\n";
                        break;
                    case 4:
                        ReturnText = "Sure hope you do.\n";
                        break;
                }
                continueDisplay = false;
            }
        }

        public sealed class OceanAccessMenu : Character
        {
            public OceanAccessMenu()
            {
                Art =
          @"
                             _ 
                  V        _/=\_               ,^._______,^.
  V                      _/=====\_            .|
                           '-\-'           ,-`|-|   |-|
                           ' ,=~U      ,-~'  _|_|___|_|_
            V             __)-(___  ,-`     /IIIIIIIIIII\
                         /  <\/>  \'    ____||_________||___
------------------------/ /|  : |\ \_.  |[][][][][ ][][][][]
         -      -   _  / / |  : | \ \ `-|___________________
  ~            ~    \\/_/  }===={  \)() /__/____/____/____/_
      ~     -        (('0  |)  (|   || _______________ _____
   -          -     -  \\  |    |   ||/____________(___|||||
        ~   ,___________`\ |  | |___||_________________|||||
~          /_-______=_____\|  | |___||___-______;______|()||
------------------------------------------------------------
                ";

                Text = "With a boat, of course, hahahah! Well, it would've been easier back in the day, when trawlers were a dime a dozen around here. " +
                "They'd let just about anyone hop in before casting off. There's good angling offshore, too...\n" +
                "I guess you could try asking the crew over at that fancy vessel nicely, and if that's not an option, maybe some of the newcomers around " +
                "here might have a seaworthy vessel you could hop into.";

                options = new string[]
                {
                    "\"You think the crew at the research vessel would take me to the ocean?\"",
                    "\"How could I attract new people to the village?\"",
                    "\"You think an ocean trawler crew would take me to the ocean?\"",
                    "\"Thanks for letting me know.\"",
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "I'm not sure. They seem to cast off once, maybe twice a month, and it's consistent. " +
                        "They have a schedule of some kind, so I don't think they'd lift their anchors spontaneously. You're free to ask them, though.\n";
                        break;
                    case 2:
                        ReturnText = "It's simple enough. Keep the locals well fed and healthy. I can help you with the former, and the village elder " +
                        "might help you with the latter. \nKeep that up and more people will come. We're not the only ones who reminisce the old days, " +
                        "it's just that some of us couldn't stand living here anymore.\n";
                        break;
                    case 3:
                        ReturnText = "Aye, without a doubt. They'd probably be honored to have a mayor of our ol' town on deck.\n";
                        break;
                    case 4:
                        ReturnText = "You're welcome.\n";
                        break;
                }
                continueDisplay = false;
            }
        }
    }

    public class Trawler : Character
    {
        private const string BackText = "Hey. Need anything else?";
        public Trawler()
        {
            Art =
          @"

                          ...
                      .-``   `--.                        V
                      ``---`````       .--.
                                     _/__  )  
                                      0)0`>|_  
              V                 /V\   \-_.-_ `; 
                               /'_/\_ /_.   './    V
 V                      V     ;._ `/ ``      |  
                 V            |^ '-;._   _.' |  
`~~^~^~^~^~^~^~^^~^~^~^~^~^~^~|^ ^  ||```    |^~^~^~^~^~^~~`
_,____._____________________.'| ^  ^||       |______________
T|\__/|__T________T________`'`|^ ^ ^|\__,.--;'____T________T
 |/__\|       /       /    ```| ^ ^ | |     | /           /
------------------------------------------------------------
            ";

            Text = "Hey. Nice view, huh?";

            options = new string[]
            {
                "\"That fish is huge.\"",
                "\"So, what's your story?\"",
                "\"How can I help you?\"",
                "\"Could you help me out with assigning fishing tasks for people?\"",
                "\"Alright, see you.\""
            };
        }

        public override void ParseOption(int option)
        {
            switch (option)
            {
                case 1:
                    Console.Clear();
                    BigFishMenu bigFishMenu = new();
                    bigFishMenu.Display();
                    Text = bigFishMenu.ReturnText;
                    Console.Clear();
                    Console.WriteLine(Art);
                    Console.WriteLine(Text);
                    break;
                case 2:
                    Console.Clear();
                    StoryMenu storyMenu = new();
                    storyMenu.Display();
                    Text = storyMenu.ReturnText;
                    Console.Clear();
                    Console.WriteLine(Art);
                    Console.WriteLine(Text);
                    break;
                case 3:
                    Console.Clear();
                    HelpMenu helpMenu = new();
                    helpMenu.Display();
                    Text = helpMenu.ReturnText;
                    Console.Clear();
                    Console.WriteLine(Art);
                    Console.WriteLine(Text);
                    break;
                case 4:
                    Console.Clear();
                    AssignmentMenu assignmentMenu = new();
                    assignmentMenu.Display();
                    Text = assignmentMenu.ReturnText;
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

        public sealed class BigFishMenu : Character
        {
            public BigFishMenu()
            {
                Art =
          @"

                          ...
                      .-``   `--.                        V
                      ``---`````       .--.
                                     _/__  )  
                                      0)0`>|_  
              V                 /V\   \-_.-_ `; 
                               /'_/\_ /_.   './    V
 V                      V     ;._ `/ ``      |  
                 V            |^ '-;._   _.' |  
`~~^~^~^~^~^~^~^^~^~^~^~^~^~^~|^ ^  ||```    |^~^~^~^~^~^~~`
_,____._____________________.'| ^  ^||       |______________
T|\__/|__T________T________`'`|^ ^ ^|\__,.--;'____T________T
 |/__\|       /       /    ```| ^ ^ | |     | /           /
------------------------------------------------------------
            ";

                Text = "Yeah, a \"big ol' lunker\", as the boys onshore would say. This one's from our last haul.";

                options = new string[]
                {
                    "\"How often do you catch fish this big?\"",
                    "\"Are the fish usually this big around here?\"",
                    "\"Nice. I wanted to ask about something else.\"",
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "I guess we catch something like that often enough that I happen to be handling a fresh one every time we speak. " +
                        "Kinda weird when you think about it.\n";
                        break;
                    case 2:
                        ReturnText = "Nah, Emberbay's actually pretty bad when it comes to trawls. My crew operates in other regions as well, and this " +
                        "fish here is from further south. I'm not from around here, but I heard that the village got messed up in the past by pollution " +
                        "or something, so that's probably why the marine life here is so sporadic.\n";
                        break;
                    case 3:
                        ReturnText = "Shoot.\n";
                        break;
                }
                continueDisplay = false;
            }
        }

        public sealed class StoryMenu : Character
        {
            public StoryMenu()
            {
                Art =
          @"

                          ...
                      .-``   `--.                        V
                      ``---`````       .--.
                                     _/__  )  
                                      0)0`>|_  
              V                 /V\   \-_.-_ `; 
                               /'_/\_ /_.   './    V
 V                      V     ;._ `/ ``      |  
                 V            |^ '-;._   _.' |  
`~~^~^~^~^~^~^~^^~^~^~^~^~^~^~|^ ^  ||```    |^~^~^~^~^~^~~`
_,____._____________________.'| ^  ^||       |______________
T|\__/|__T________T________`'`|^ ^ ^|\__,.--;'____T________T
 |/__\|       /       /    ```| ^ ^ | |     | /           /
------------------------------------------------------------
            ";

                Text = "Uh... It's nothing interesting, honestly.";

                options = new string[]
                {
                    "\"Nothing at all? There has to be something of interest.\"",
                    "\"Okay. What's the craziest thing you've done in your life?\"",
                    "\"Just trying to strike up a conversation here...\"",
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "I mean, people don't usually ask me stuff like that. My life story is mostly boring, and not really any of your business. No offense.\n";
                        break;
                    case 2:
                        ReturnText = "Look, we've basically just met. I'd rather not share that information with just anybody, if you know what I mean.\n";
                        break;
                    case 3:
                        ReturnText = "Ok, I see. I'm a little busy working, so apologies.\n";
                        break;
                }
                continueDisplay = false;
            }
        }

        public sealed class HelpMenu : Character
        {
            public HelpMenu()
            {
                Art =
          @"

                          ...
                      .-``   `--.                        V
                      ``---`````       .--.
                                     _/__  )  
                                      0)0`>|_  
              V                 /V\   \-_.-_ `; 
                               /'_/\_ /_.   './    V
 V                      V     ;._ `/ ``      |  
                 V            |^ '-;._   _.' |  
`~~^~^~^~^~^~^~^^~^~^~^~^~^~^~|^ ^  ||```    |^~^~^~^~^~^~~`
_,____._____________________.'| ^  ^||       |______________
T|\__/|__T________T________`'`|^ ^ ^|\__,.--;'____T________T
 |/__\|       /       /    ```| ^ ^ | |     | /           /
------------------------------------------------------------
            ";

                Text = "As in, with the trawling? I'm guessing you're not able to help us full time, and we could use some more hands on deck.\n" +
                "You could probably get some of the locals to help us, though. As long as they don't get seasick easily, they could help us process " +
                "fish at a faster rate, and we can donate more of our catch to Emberbay. We're nice like that, you see.";

                options = new string[]
                {
                    "\"So you don't donate all caught fish to the village?\"",
                    "\"Anything we can do in terms of pollution cleanup here?\"",
                    "\"So, the more villagers I allocate, the more fish you can catch?\"",
                    "\"Right. I had another question.\"",
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "Well, no, I've got other clients too. Gotta keep this ship floating somehow, right?\n";
                        break;
                    case 2:
                        ReturnText = "I wouldn't really know, sorry. I've hardly noticed any pollution, but then again I haven't paid much attention.\n";
                        break;
                    case 3:
                        ReturnText = "Yup. You want to be careful, though, since the ecosystem here is so anemic you might actually kill it off if you " +
                        "assign too many villagers to trawl at once. I saw a research vessel anchored in Emberbay not long ago, they usually track this kind " +
                        "of stuff so you might want to consult them before making any assignments.\n";
                        break;
                    case 4:
                        ReturnText = "What is it?\n";
                        break;
                }
                continueDisplay = false;
            }
        }

        public sealed class AssignmentMenu : Character
        {
            public AssignmentMenu()
            {
                Art =
          @"

                          ...
                      .-``   `--.                        V
                      ``---`````       .--.
                                     _/__  )  
                                      0)0`>|_  
              V                 /V\   \-_.-_ `; 
                               /'_/\_ /_.   './    V
 V                      V     ;._ `/ ``      |  
                 V            |^ '-;._   _.' |  
`~~^~^~^~^~^~^~^^~^~^~^~^~^~^~|^ ^  ||```    |^~^~^~^~^~^~~`
_,____._____________________.'| ^  ^||       |______________
T|\__/|__T________T________`'`|^ ^ ^|\__,.--;'____T________T
 |/__\|       /       /    ```| ^ ^ | |     | /           /
------------------------------------------------------------
            ";

                Text = "Yeah, sure. Just give us the word and we'll let the other trawlers know.";

                options = new string[]
                {
                    "\"I'll need a list of all local fish, and I'll assign a number of people to catch each fish type. Would that work?\"",
                    "\"How many fish would each villager be able to catch per month?\"",
                    "\"What's the risk of accidentally overfishing a fish type that wasn't assigned to anyone?\"",
                    "\"Good to hear.\"",
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "We'll make it work. Though this is the ocean, so I won't be able to list ALL the fish species we tend to catch.\n" +
                        "I'll add the most common types that can be tracked easier, though I can't guarantee that something else won't pop up in bycatch.\n";
                        break;
                    case 2:
                        ReturnText = "After we split the haul, each villager should be able to take about a hundred fish per month or so, " +
                        "maybe add another half a dozen in bycatch. Though some fish are harder to catch than others, of course.\n";
                        break;
                    case 3:
                        ReturnText = "Well, we trawl, so we end up catching all kinds of stuff. We use nets that allow for limited catch and release, " +
                        "but I'd say that in short, the more villagers are assigned, the higher the bycatch will be, and therefore the risk you've " +
                        "mentioned will be higher.\n";
                        break;
                    case 4:
                        ReturnText = "Yup.\n";
                        break;
                }
                continueDisplay = false;
            }
        }
    }

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
