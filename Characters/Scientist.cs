namespace ShoresOfEmberbay
{
    public class Scientist : Character
    {
        private const string BackText = "Welcome again, Mayor.";
        public Scientist()
        {
            Art = GameArt.Scientist;
            Text = "Good to see you Mayor, what can I do for you?";
            options = new string[] {
                "\"Can you tell me more about which fish species I can fish?\"",
                "\"Can I go with you and your crew on the Research Vessel to the Ocean?\"",
                "\"Can you tell me more about water pollution?\"",
                "\"How can you help me?\"",
                "\"Goodbye.\""
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
                    ReturnTextChangeMenu3 returnTextChangeMenu3 = new();
                    returnTextChangeMenu3.Display();
                    Text = returnTextChangeMenu3.ReturnText;
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
                Art = GameArt.Scientist;
                Text = "With great pleasure. You can fish in two locations - the Docks and the Ocean. " +
                    "\nMy crew and I found around 14 different species in this area. " +
                    "\nHowever some of the fish are endangered species and by catching them you can contribute to the extinction of the entire population." +
                    "\nYou have to be careful when going fishing with villagers. ";


                options = new string[]
                {
                    "\"I would like to hear more about endangered species.\"",
                    "\"Thank you for your information and time. Now I know what to look for.\"",
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "Endangered species means that the population of that type of fish is drastically dropping or stay at the same vulnerable low level." +
                            "\nTo prevent that, fishing for a given species should be limited or completely stopped. \n";
                        break;
                    case 2:
                        ReturnText = "I am glad I could help.\n";
                        break;
                }
                continueDisplay = false;
            }
        }
        public sealed class ReturnTextChangeMenu : Character
        {
            public ReturnTextChangeMenu()
            {
                Art = GameArt.Scientist;
                Text = "Unfortunately, there is no such possibility." +
                    "\nWe do special expeditions only once a month and I take only trained people on board.";
                options = new string[]
                {
                    "\"I understand, I didn't know that.\"",
                    "\"Okay, I will find another way.\"",
                };
            }
            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "I'm sorry I can't help.\n";
                        continueDisplay = false;
                        break;
                    case 2:
                        ReturnText = "I am sure you will.\n";
                        continueDisplay = false;
                        break;
                }
            }
        }
        public sealed class ReturnTextChangeMenu2 : Character
        {
            public ReturnTextChangeMenu2()
            {
                Art = GameArt.Scientist;
                Text = "Of course. Water pollution in the ocean is caused by the release of harmful materials such as plastic waste," +
                    "\nmicroplastics, and phosphorus into the water. The pollution can have devastating effects on the marine life, " +
                    "\nincluding fish, plants, and other animals living in the ocean. Therefore, it is essential to be mindful of " +
                    "\nthe substances we release into the ocean to keep it clean and safe for everyone.";

                options = new string[]
                {
                    "\"Plastic waste? How can we clean that?\"",
                    "\"How could the phosphorus be removed?\"",
                    "\"Is there any way to clean the water from microplastics?\"",
                    "\"That sounds like a big problem, but I have more important things on my mind.\""
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "Ah, that is simple. Large scale plastic waste can be removed from the water by hand or using a simple net." +
                            "\nI am sure your village has plenty of those at your disposal.\n";
                        break;
                    case 2:
                        ReturnText = "Using chemical dosing with metal salts, it is possible to remove excess phosphorus from the ocean." +
                            "\nThe elder of the village was working on acquiring some for us a while ago.\n";
                        break;
                    case 3:
                        ReturnText = "There is, in theory. A sifter that uses a porous membrane filter could remove microplastic waste, " +
                            "\nbut I am unaware of any such systems being used outside of a laboratory setting.\n";
                        break;
                    case 4:
                        ReturnText = "I am always here to help if you change your mind.\n";
                        break;
                }
                continueDisplay = false;
            }
        }
        public sealed class ReturnTextChangeMenu3 : Character
        {
            public ReturnTextChangeMenu3()
            {
                Art = GameArt.Scientist;
                Text = "I can share some information about the ocean and fishing, water pollution and how to prevent it. \n" +
                "My team compiles statistics regarding the ocean every month. If you wish to see this *info*, ask me any time you're in the vessel.";

                options = new string[]
                {
                     "\"Sounds great. I wish to expand my knowledge within this subject.\"",
                     "\"Okay, thank you.\""
                };
            }
            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "I am glad to hear that.\n";
                        break;
                    case 2:
                        ReturnText = "I am always here to help.\n";
                        break;
                }
                continueDisplay = false;
            }
        }
    }
}
