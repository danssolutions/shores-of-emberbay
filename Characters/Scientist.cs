namespace ShoresOfEmberbay
{
    public class Scientist : Character
    {
        private const string BackText = "Welcome again, Mayor.";
        public Scientist()
        {
            Art = GameArt.Scientist;
            Text = "Good to see you Mayor, what can I do for you?";


            options = new string[]{

            "\"Can you tell me more about which fish species I can fish?\"",
            "\"Can I go with you and your crew on the Research Vessel to the Ocean?\"",
            "\"Can you tell me more about water pollution?\"",
            "\"How can you help me?\"",
            "\"Goodbye\""

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
                Text = "With great pleasure. You can fish in two locations - the Docks and the Ocean.  " +
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
                        ReturnText = "Endangered species means that the popullaion of that type of fish is drastically dropping or stay at the same vulnerable low level." +
                            "\nTo prevent that, fishing for a given species should be limited or completly stopped. ";
                        break;
                    case 2:
                        ReturnText = "I am glad I could help.";
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
                        ReturnText = "I'm sorry I can't help.";
                        continueDisplay = false;
                        break;
                    case 2:
                        ReturnText = "I am sure you will.";
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
                Text = "Water pollution in the ocean is caused by the release of harmful substances such as oil spills," +
                    "\nplastic waste, and chemicals into the water. The pollution can have devastating effects on the marine life, " +
                    "\nincluding fish, plants, and other animals living in the ocean. Therefore, it is essential to be mindful of " +
                    "\nthe substances we release into the ocean to keep it clean and safe for everyone.";

                options = new string[]
                {
                    "\"Is there any way to clean the water?\"",
                    "\"That sounds like a big problem, but I have more important things on my mind.\""
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "Actually yes. Somewhere in this area it should be possible to get algea cleaner, which you can use to clean the water" +
                            "\nI would recommend going to the Village Elder since she knows best where everything is.";
                        break;
                    case 2:
                        ReturnText = "I am always here to help if you change your mind.";
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
                Text = "I can share some information about the ocean and fishing, water pollution and how to prevent it.";
                
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
                        ReturnText = "I am glad to hear that.";
                        break;
                    case 2:
                        ReturnText = "I am always here to help.";
                        break;
                }
                continueDisplay = false;

            }
        }
        
    }
    

}
