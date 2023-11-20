namespace TownOfZuul
{
    public class Ending
    {
        public bool CheckEnding()
        {
            // check win/fail condition here
            return false;
        }

        public void ShowGoodEnding()
        {
            string endingArt1 = @"


                       Ending :)
         
            Placeholder art
                                                                     
---------------------------------------------------------
                                                                     
            ";
            string endingArt2 = @"


                       Ending :)
  
            Placeholder art
                                                                     
---------------------------------------------------------
                                                                     
            ";
            string endingArt3 = @"


                       Ending :(
         
            Placeholder art
                                                                     
---------------------------------------------------------
                                                                     
            ";
            string endingArt4 = @"


                       Ending :(
         
            Placeholder art
                                                                     
---------------------------------------------------------
                                                                     
            ";
            string endingText1 = 
            "Ending text 1\n";
            string endingText2 = 
            "Ending text 2\n";
            string endingText3 = 
            "Ending text 3\n";
            string endingText4 = 
            "Ending text 4\n";
            GenericMenu endingSlide1 = new(endingArt1, endingText1);
            endingSlide1.Display();
            GenericMenu endingSlide2 = new(endingArt2, endingText2);
            endingSlide2.Display();
            GenericMenu endingSlide3 = new(endingArt3, endingText3);
            endingSlide3.Display();
            GenericMenu endingSlide4 = new(endingArt4, endingText4);
            endingSlide4.Display();
        }

        public void ShowBadEnding()
        {
            
        }

        public void ShowGameOver()
        {
            
        }
    }
}