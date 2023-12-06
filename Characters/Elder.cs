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
                "\"Can you tell me more about the village?\"",
                "\"Can you help me with some directions?\"",
                "\"How can you help me?\"",
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
                    CoastToolsMenu coastToolsMenu = new();
                    CoastCleanMenu CoastCleanMenu = new();
                    NutrientToolsMenu nutrientToolsMenu = new();
                    NutrientsCleanMenu NutrientsCleanMenu = new();

                    if (nutrientCleaningDiscussed)
                    {
                        string allUnlockedText = "You have all ocean cleanup tools you can get.";
                        GenericMenu allUnlocked = new(GameArt.Elder, allUnlockedText);
                        allUnlocked.Display();
                    }
                    else if (nutrientsCleaned)
                    {
                        NutrientsCleanMenu.Display();
                        Text = NutrientsCleanMenu.ReturnText;
                        nutrientCleaningDiscussed = true;
                        Console.Clear();
                        Console.WriteLine(Art);
                        Console.WriteLine(Text);
                    }
                    else if (coastCleaningDiscussed)
                    {
                        nutrientToolsMenu.Display();
                        Text = nutrientToolsMenu.ReturnText;
                        Console.Clear();
                        Console.WriteLine(Art);
                        Console.WriteLine(Text);
                    }
                    else if (coastCleaned)
                    {
                        CoastCleanMenu.Display();
                        Text = CoastCleanMenu.ReturnText;
                        coastCleaningDiscussed = true;
                        Console.Clear();
                        Console.WriteLine(Art);
                        Console.WriteLine(Text);
                    }
                    else
                    {
                        coastToolsMenu.Display();  
                        Text = coastToolsMenu.ReturnText;
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

        public sealed class StoryMenu : Character
        {
            public StoryMenu()
            {
                Art = GameArt.Elder;
                Text = "When I was just a child 60 years ago the village was thriving. " +
                "\nNow we are just trying to survive. Our health is getting worse for everyday, because we either don't get anything to eat " +
                "\nor because the fish we eat are contaminated with plastic or other chemicals. " +
                "\nIndustries took our fish so we barely have enough food and we have to be careful deciding what fish to catch. " +
                "\nThey polluted our water. They are slowly moving away from our area, " +
                "\nbut now we need to think about what fish we catch and eat fish from polluted water.";

                options = new string[]
                {
                    "\"I will do everything in my power to help the village. Do you have some advice?\"",
                    "\"Maybe we should focus on other resources instead of what's available from the water?\"",
                    "\"Thank you for sharing your story. You should have been the Mayor of the village.\"",
                    "\"Do you think it is too late to change things for the better?\"",
                    "\"To be honest, I do not think there are any problems with the village right now.\"",
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "I advise you to take into consideration what you fish and to treat bodies of water with respect.\n"
                        + "You can also talk with the scientist, I am sure he has some valuable insight.\n";
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
                        ReturnText = "I believe that it is still possible, but only by immediate action.\n";
                        break;
                    case 5:
                        ReturnText = "I hope that after looking around, you will change your mind.\n";
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
                Text = "Of course, what location do you need directions to?";

                options = new string[]
                {
                    "\"Where are the village docks?\"",
                    "\"How can I get to the ocean?\"",
                    "\"I saw a research vessel docked nearby. How can I get to it?\"",
                    "\"I have records of a wastewater treatment plant near the village. Where is it located?\"",
                    "\"Where is the nearest coast?\"",
                    "\"Where is the village?\""
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "The village docks are to the east from here. You can't miss them.\n";
                        continueDisplay = false;
                        break;
                    case 2:
                        ReturnText = "The ocean is to the far east of the village. You will need to use a boat of some kind to get there.\n";
                        continueDisplay = false;
                        break;
                    case 3:
                        ReturnText = "The research vessel is at the northern part of the village docks, which are east of the village itself.\n";
                        continueDisplay = false;
                        break;
                    case 4:
                        ReturnText = "The Wastewater Treatment Plant is at the far south of the village. Keep going south from there and you'll find it.\n";
                        continueDisplay = false;
                        break;
                    case 5:
                        ReturnText = "The coast is south of the village. You can't miss it.\n";
                        continueDisplay = false;
                        break;
                    case 6:
                        ReturnText = "...the Village is to the east of here. You came from the village, so just go *back* the way you came from.\n";
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
                Text = "I am delighted to know that someone here appreciates the knowledge of en elder. "
                + "How can I help?";

                options = new string[]
                {
                    "\"What places would you recommend me to visit in the Village?\"",
                    "\"Would you be able to help me find some useful tools for ocean cleanup?\"",
                    "\"How can I improve the Village?\"",
                    "\"Whom can I ask for help?\""
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "I would recommend visiting the Docks and the Wastewater Plant, and, when possible, the Ocean.\n";
                        continueDisplay = false;
                        break;
                    case 2:
                        ReturnText = "Yes, I have a few tools for ocean cleanup in mind that I'm working on getting for you.\n" +
                        "In the meantime, focus on what you can do right now, with the tools you currently have.\n";
                        continueDisplay = false;
                        break;
                    case 3:
                        ReturnText = "To improve a Village you must improve the lives of the people in the Village. "
                    + "\nThe overall health is decreasing. People are getting sick. The water needs to be cleaned, "
                    + "\nfor the Villagers to be able to drink, eat and work, thereby becoming happy and healthy.\n";
                        continueDisplay = false;
                        break;
                    case 4:
                        ReturnText = "I am always here to help, "
                    + "\nbut the intelligent scientist at the research vessel might also be of great help.\n";
                        continueDisplay = false;
                        break;
                }
            }
        }

        public sealed class CoastToolsMenu : Character
        {
            public CoastToolsMenu()
            {
                Art = GameArt.Elder;
                Text = "What task do you need these tools for? I will check if I have them.";

                options = new string[]
                {
                    "\"I was wondering if you have some tools for cleaning the garbage more efficiently.\"",
                    "\"I could really use some tools to clean the polluting nutrients from the water.\""
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
                        ReturnText = "I am currently working on sourcing the chemical solution needed to purify the water from nutrient pollution. \n" +
                        "If you come back later, it might be ready for you. In3 the meantime maybe you can work on cleaning the garbage from the coast.\n";
                        continueDisplay = false;
                        break;
                }
            }
        }

        public sealed class NutrientToolsMenu : Character
        {
            public NutrientToolsMenu()
            {
                Art = GameArt.Elder;
                Text = "What kind of tools do you have in mind? I already gave you the chemical solution you need to clean up nutrient pollution.";

                options = new string[]
                {
                    "\"I was wondering if you have some tools for cleaning the garbage more efficiently.\"",
                    "\"I need tools for cleaning microplastic pollution. Do you have anything for that?\"",
                    "\"Maybe there's a way to restore that old wastewater plant?\""
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
                        "If you come back later, it might be ready for you. In the meantime, maybe you can work on cleaning the nutrients from the ocean.\n";
                        continueDisplay = false;
                        break;
                    case 3:
                        ReturnText = "I have a plan for using the old wastewater plant to clean the water, but it is still in the works.\n" +
                        "I can share the details with you when I'm ready. In the meantime, maybe you can work on cleaning the nutrients from the ocean.\n";
                        continueDisplay = false;
                        break;
                }
            }
        }

        public sealed class CoastCleanMenu : Character
        {
            public CoastCleanMenu()
            {
                Art = GameArt.Elder;
                Text = "Yes, actually. I've just acquired a chemical solution needed to clean the water from nutrient pollution!";

                options = new string[]
                {
                    "\"Great to hear. Now would be a great time to upgrade to more advanced tools to clean the water.\"",
                    "\"I was actually thinking if you might have found other helpful tools.\""
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "You are welcome, Mayor. I will talk to the research vessel crew to see how we can use this new tool. \n" +
                        "You might want to talk to the scientist next month to see if it is ready for use. I hope it will be useful.\n";
                        continueDisplay = false;
                        break;
                    case 2:
                        ReturnText = "I have not found any other tools, but I will make sure we get the nutrient cleaning solution set up. \n" +
                        "You might want to talk to the scientist in the research vessel next month to see if it is ready for use.\n";
                        continueDisplay = false;
                        break;
                }
                continueDisplay = false;
            }
        }

        public sealed class NutrientsCleanMenu : Character
        {
            public NutrientsCleanMenu()
            {
                Art = GameArt.Elder;
                Text = "I am glad you asked, because I have great news! \nI have finally acquired all necessary parts for an experimental " +
                "membrane filter that can remove microplastics from the local water.";

                options = new string[]
                {
                    "\"That is exactly what I am looking for. I am hoping that you can help again.\"",
                    "\"Wow. How can we use it? Does it need to be installed somewhere?\"",
                    "\"I was actually thinking if you might have found other helpful tools.\""
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "That is great. My plan is to set up this filter in the old wastewater plant with help of the locals. \n" +
                        "It shouldn't take us more than a month to have it ready. \nCome visit the plant next month, I am confident it will be done by then.\n";
                        continueDisplay = false;
                        break;
                    case 2:
                        ReturnText = "The old wastewater plant should be a perfect location to set it up, " +
                        "as it already has the necessary flow systems built in. \nNot like that old plant is useful for anything else, anyway.\n" +
                        "It shouldn't take us more than a month to have it ready. Hope it can help you make good progress.\n";
                        continueDisplay = false;
                        break;
                    case 3:
                        ReturnText = "I don't think we will get better tools than this. \nI will make sure we get the microplastic cleaning solution set up. " +
                        "You might want to visit the wastewater plant next month to see if the new filter is ready for use.\n";
                        continueDisplay = false;
                        break;
                }
                continueDisplay = false;
            }
        }
    }
}
