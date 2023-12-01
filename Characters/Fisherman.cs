namespace ShoresOfEmberbay
{
    public class Fisherman : Character
    {
        private const string BackText = "Ahoy, mayor. Fair winds.";
        public Fisherman()
        {
            Art = GameArt.Fisherman;
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
                Art = GameArt.Fisherman;
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
                Art = GameArt.Fisherman;
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
                Art = GameArt.Fisherman;
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
                Art = GameArt.Fisherman;
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
}
