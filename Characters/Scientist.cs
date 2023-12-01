namespace TownOfZuul
{
    public class Scientist : Character
    {
        private const string BackText = "Welcome again, Major.";
        public Scientist()
        {
            Art = GameArt.Scientist;
            Text = "Good to see you Major, what can I do for you?";

            options = new string[]{

            "\"Can you tell me more about fish species that I can fish?\"",
            "\" here will be one more option\"",
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
                    //TextChangeMenu textChangeMenu = new();
                    //textChangeMenu.Display();
                    //Text = textChangeMenu.ReturnText;
                    Console.Clear();
                    Console.WriteLine(Art);
                    Console.WriteLine(Text);
                    break;
                case 2:
                    Console.Clear();
                    //ReturnTextChangeMenu returnTextChangeMenu = new();
                    //returnTextChangeMenu.Display();
                    //Text = returnTextChangeMenu.ReturnText;
                    Console.Clear();
                    Console.WriteLine(Art);
                    Console.WriteLine(Text);
                    break;
                case 3:
                    Console.Clear();
                    //ReturnTextChangeMenu2 returnTextChangeMenu2 = new();
                    //returnTextChangeMenu2.Display();
                    //Text = returnTextChangeMenu2.ReturnText;
                    Console.Clear();
                    Console.WriteLine(Art);
                    Console.WriteLine(Text);
                    break;
                case 4:
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

        public sealed class TextCHangeMenu : Character
        {
            public TextCHangeMenu()
            {
                Art = GameArt.Scientist;
                Text = "story of sceintisc here";

                options = new string[]
                {
                    "\"one\"",
                    "\"second\"",
                };
            }

            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "";
                        break;
                    case 2:
                        ReturnText = "second thing";
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
                Text = "here we have more stuff";

                options = new string[]
                {
                "\"here\""

                };
            }
            public override void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ReturnText = "...";
                        continueDisplay = false; //do we need that?
                        break;
                }
            }
        }
    }
}
