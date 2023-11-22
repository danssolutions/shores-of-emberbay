namespace TownOfZuul
{
    public class Ending
    {
        // Menu used in Good Ending final choice to continue game or return to main menu
        public class EndingMenu : Menu
        {
            public bool ContinueGame { get; private set; } = false;
            private const string Art = GameArt.AdvanceMonth;
            private const string Text = "After their tenure, the mayor ended up writing a bestseller about their experience and associated tips for future mayors of cities and towns worldwide. \n\nThe innovative application of sustainable development resulted in the mayor attending numerous interviews, discussing future goals and suggestions for aspiring city developers. \n\nDuring the last interview scheduled for the year, an interesting question is presented: \n\n“Are there any plans to return to the village and become mayor again?”\n";

            public EndingMenu()
            {
                options = new string[] {
                    "'Yep.' (Continue Playing)",
                    "'Nah.' (Go to Main Menu)"
                };
            }

            override public void Display()
            {
                Console.Clear();

                Console.WriteLine(Art);
                Console.WriteLine(Text);

                base.Display();
            }

            override public void ParseOption(int option)
            {
                switch (option)
                {
                    case 1:
                        ContinueGame = true;
                        continueDisplay = false;
                        break;
                    case 2:
                        ContinueGame = false;
                        continueDisplay = false;
                        break;
                }
            }
        }

        // Check win condition and determine ending based on whether player met it.
        // Returns true if game should continue, otherwise returns false.
        public static bool GetEnding(uint populationCount, double populationHealth)
        {
            if (populationCount >= 400 && populationHealth >= 0.95)
            {
                ShowGoodEndingSlides();
                EndingMenu endingMenu = new();
                endingMenu.Display();
                return endingMenu.ContinueGame;
            }
            else
            {
                ShowBadEndingSlides();
                return false;
            }
        }

        public static void ShowGoodEndingSlides()
        {
            string villageText =
            "And so, once the villagers took matters into their own hands under the new mayor’s leadership, their settlement changed forever. \n\nWhat used to be a fallen remnant of great times past is now prospering, becoming an inspiring symbol of harmony between humanity and nature.\n";
            string docksText =
            "The village docks, once a sad reminder of the settlement’s past glory, have become fully revitalized under the new mayor’s tenure. \n\nNumerous vessels coming and passing by the village have resulted in the harbour becoming a hub for travellers, traders and fishermen alike, \nserving as the core of the village’s resources and culture.\n";
            string fisherText =
            "The local fishermen, now aware of sustainable fishing practices, have used their knowledge to satisfy the village’s food necessities \nwhile keeping the local marine environment healthy and biodiverse. \n\nSome local sailors say that marine life has become so plentiful that you can see dozens, \neven hundreds of fish swimming in the clear waters at a single time with the naked eye.\n";
            string elderText =
            "As a new day had dawned, the elder of the village gazed out the window of her homestead, as was her routine. \n\nHowever, the view outside was no longer the same as she was used to seeing lately. \nThe sight of the village, bathed in sunrise, appeared colorful and majestic, reminiscent of how the village seemed to look like in old memories. \n\nThe elder couldn't help but smile - for the first time in a long while, she felt true happiness and contentment. \n\nThe village had truly come back from its slumber.\n";
            string coastText =
            "With time and effort, the coast has been cleaned of the pollution which threatened the well-being of the animals and tainted its beauty. \n\nIt is now a popular spot for adventurous young folk, especially those interested in ocean preservation.\n";
            string scientistText =
            "When conditions were finally considered optimal, the scientists and crew aboard the local research vessel took off towards the deep ocean. \n\nThe villagers thought the ship would never return, until it appeared on the horizon many months after its departure. \n\nThe discoveries by the research team were astonishing - according to measurements, the local marine environment had become one of the least polluted in the world. \n\nIn addition, numerous new species of fish were discovered as part of the expedition, which brought great acclaim to the operation and its crew.\n";
            string wastePlantText =
            "After the old wastewater treatment plant was restored and refurbished, its novelty and efficiency gained a substantial amount of news coverage. \n\nIts technology and application inspired many more innovations in the field of marine preservation and helped kickstart ocean cleanup projects across the globe. \n";
            string trawlerText =
            "Throughout the last few months, the local trawlers have been catching record amounts of various diverse types of fish in the nearby ocean, \nmaking sure to implement sustainable trawling practices as part of their craft. \n\nThe efforts of the trawlers will ensure that the village remains well fed and develops sustainably for a long time to come.\n";
            GenericMenu villageSlide = new(GameArt.Village, villageText);
            villageSlide.Display();
            GenericMenu docksSlide = new(GameArt.Docks, docksText);
            docksSlide.Display();
            GenericMenu fisherSlide = new(GameArt.Docks, fisherText);
            fisherSlide.Display();
            GenericMenu elderSlide = new(GameArt.ElderHouse, elderText);
            elderSlide.Display();
            GenericMenu coastSlide = new(GameArt.Coast, coastText);
            coastSlide.Display();
            GenericMenu scientistSlide = new(GameArt.ResearchVessel, scientistText);
            scientistSlide.Display();
            GenericMenu wastePlantSlide = new(GameArt.WastePlant, wastePlantText);
            wastePlantSlide.Display();
            GenericMenu trawlerSlide = new(GameArt.Ocean, trawlerText);
            trawlerSlide.Display();
        }

        public static void ShowBadEndingSlides()
        {
            string villageText =
            "And so, under the new mayor’s leadership, the villagers regained hope and worked hard to revive the settlement. \n\nAnd yet, despite their best efforts, they were struggling to succeed, \nfor there weren't enough able bodied workers to sustain the village, \nand those who could still work frequently suffered from illness and hunger.\n";
            string docksText =
            "The village docks went through a brief rise in activity under the mayor's tenure, \nbefore the village's dwindling population once again caused the docks to remain largely unused, a shadow of its former self. \n\nThe lack of maintenance over the years eventually resulted in the harbor becoming nearly unusable.\n";
            string fisherText =
            "As the harbor slowly became worse for wear, the impacts of the deterioration hit the local fishers the hardest. \nMany locals that used to work in the harbor and supply the village with resources and food had lost their jobs, \nleaving them struggling to survive and be able to provide for their families.\n";
            string elderText =
            "As a new day had dawned, the elder of the village gazed out the window of her homestead, as was her routine. \n\nShe had hoped that the new mayor would finally turn things around, but the familiar sight of the stagnant village left her disappointed. \nShe wondered if she would ever see the village become prosperous again.\n";
            string scientistText =
            "Shortly after the mayor's tenure, the villagers witnessed the research vessel in the docks taking off towards the deep ocean. \n\nNobody saw the ship ever return.\n";
            string mayorText =
            "Despite the fate of the settlement, the mayor was never viewed in a negative light. \nAfter all, they brought hope to the villagers that eventually, things would get better, \nand the sustainable development practices utilized by the mayor could be fully realized by new leadership. \nSomeday, the village will prosper once again.\n\n - THE END -\n";
            GenericMenu villageSlide = new(GameArt.Village, villageText);
            villageSlide.Display();
            GenericMenu docksSlide = new(GameArt.Docks, docksText);
            docksSlide.Display();
            GenericMenu fisherSlide = new(GameArt.Docks, fisherText);
            fisherSlide.Display();
            GenericMenu elderSlide = new(GameArt.ElderHouse, elderText);
            elderSlide.Display();
            GenericMenu scientistSlide = new(GameArt.ResearchVessel, scientistText);
            scientistSlide.Display();
            GenericMenu mayorSlide = new(GameArt.AdvanceMonth, mayorText);
            mayorSlide.Display();
        }

        public static void ShowGameOverSlides()
        {
            string mayorText =
            "The mayor woke up one day to witness the village being completely deserted. It's as if everyone left all at once. \nAfter fruitlessly searching for any living soul in the surrounding areas, \nthe mayor had to give up and request their superiors for evacuation.\n";
            string elderText =
            "The elder of the village ended up becoming its last remaining inhabitant. \nWith her old age and poor health, she knew her demise would come soon, \nand vowed to commemorate the village and its former residents until the very end.\n";
            string fisherText =
            "For a while there existed a rumor among the local urban explorers, \nlegends of 'ghost' fishermen in the ruins of the village harbor. \n\nSome said that these fishermen still haunt the abandoned docks at night, \nin a futile attempt to provide for their long gone families.\n";
            string scientistText =
            "The fate of the research vessel that used to be anchored at the ancient docks remains unknown.\n";
            string villageText =
            "Over the years, the village was forgotten to time, it's only signs of existence being scattered ruins of long gone buildings. \nIt will never get a chance to prosper again. \n\n - GAME OVER -\n";
            GenericMenu mayorSlide = new(GameArt.Village, mayorText);
            mayorSlide.Display();
            GenericMenu elderSlide = new(GameArt.ElderHouse, elderText);
            elderSlide.Display();
            GenericMenu fisherSlide = new(GameArt.Docks, fisherText);
            fisherSlide.Display();
            GenericMenu scientistSlide = new(GameArt.ResearchVessel, scientistText);
            scientistSlide.Display();
            GenericMenu villageSlide = new(GameArt.Coast, villageText);
            villageSlide.Display();
        }
    }
}