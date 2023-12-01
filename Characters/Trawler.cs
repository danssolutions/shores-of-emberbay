namespace ShoresOfEmberbay
{
    public class Trawler : Character
    {
        private const string BackText = "Hey. Need anything else?";
        public Trawler()
        {
            Art = GameArt.Trawler;
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
                Art = GameArt.Trawler;
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
                Art = GameArt.Trawler;
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
                Art = GameArt.Trawler;
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
                Art = GameArt.Trawler;
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
}
