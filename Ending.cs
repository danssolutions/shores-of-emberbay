namespace TownOfZuul
{
    public class Ending
    {
        public bool GetEnding(uint populationCount, double populationHealth)
        {
            // Check win condition and determine ending based on whether player met it.
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

        public void ShowGoodEndingSlides()
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
            "The waste plant gained a substantial amount of news coverage because of its efficiency and novelty. \n\nIts technology and application inspired many more innovations in the field of marine preservation and helped kickstart ocean cleanup projects across the globe. \n";
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

        public void ShowBadEndingSlides()
        {
            /*
                introduction to village general desciption of misfortune
                the docks have slowly become unusable
                fisherman (if included), lost his job, struggling to survive and keep his family afloat
                village elder lies in her deathbed, sad that she never saw the village recover (cringe)
                research vessel just left lmao, the docks are no longer safe to use
                mayor's fate - their term expired, the village has remained stagnant for years to come until big oil comes in to "save" the village
            */
            Console.WriteLine("Congrats, muppet. You failed lmao");
            Thread.Sleep(5000);
        }

        public void ShowGameOverSlides()
        {
            /*
                introduction to village becoming abandoned
                no one is left in the docks, what even is a dock anyway, it got completely reclaimed by the ocean over the years
                fisherman (if included), the local urban explorers tell of a legend of a ghostly fisherman haunting what used to be the docks,
                 still trying to feed his long gone family
                village elder witnessed the last sight of the village ruins before passing away
                the fate of the research vessel near the village is unknown
                the village has become fully abandoned, eventually forgotten to time
            */
        }
    }
}