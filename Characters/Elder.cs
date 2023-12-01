namespace ShoresOfEmberbay
{
    public class Elder : Character
    {
        private bool coastCleaned = false;
        public bool coastCleaningDiscussed = false;
        private bool nutrientsCleaned = false;
        public bool nutrientCleaningDiscussed = false;
        private const string BackText = "Hello again.";
        public Elder()
        {
            Art = GameArt.Elder;
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

        public void CoastCleaned()
        {
            coastCleaned = true;
        }
        public void NutrientsCleaned()
        {
            nutrientsCleaned = true;
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
                    DirectionsMenu directionsMenu = new();
                    directionsMenu.Display();
                    Text = directionsMenu.ReturnText;
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
                    if (nutrientsCleaned)
                    {
                        Console.WriteLine("Yo you cleaned the nutrients! Have a membrane filter");
                        nutrientCleaningDiscussed = true; // unlocks membrane filter installation in wastewater plant
                    }
                    else if (coastCleaned)
                    {
                        Console.WriteLine("Yo you cleaned the coast! Have some algae cleaner");
                        coastCleaningDiscussed = true; // unlocks the phosphorus cleaner in the research vessel
                    }
                    else
                        Console.WriteLine("Hey you haven't cleaned anything!");
                    Thread.Sleep(5000);
                    // add option check here
                    // if unlocked, display one type of menu and set __CleaningDiscussed to true
                    // if still locked, display a different type of menu informing player that they still need to clean a specific location
                    /*ToolsMenu toolsMenu = new();
                    NutrientCleanMenu nutrientCleanMenu = new();

                    if (coastCleaned == false)
                    {
                        toolsMenu.Display();  
                        Text = toolsMenu.ReturnText;
                        Console.Clear();
                        Console.WriteLine(Art);
                        Console.WriteLine(Text);
                    }
                    else if (coastCleaned == true)
                    {
                        nutrientsCleaned = true;
                        nutrientCleanMenu.Display();
                        Text = nutrientCleanMenu.ReturnText;
                        Console.Clear();
                        Console.WriteLine(Art);
                        Console.WriteLine(Text);
                    }*/
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
                Art = GameArt.Elder;
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

        public sealed class DirectionsMenu : Character
        {
            
            public DirectionsMenu()
            {
                Art = GameArt.Elder;
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

        public sealed class HelpMenu : Character
        {
            public HelpMenu()
            {
                Art = GameArt.Elder;
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

        public sealed class ToolsMenu : Character
        {
            
            public ToolsMenu()
            {
                Art = GameArt.Elder;
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

    public sealed class NutrientCleanMenu : Character
        {
            
            public NutrientCleanMenu()
            {
                Art = GameArt.Elder;
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
}
