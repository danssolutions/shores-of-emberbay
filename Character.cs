namespace TownOfZuul
{
    public abstract class Character
    {
        //picture of each character
        public string? characterPicture { get; protected set; }

        //Dialogue menu
        //public menu? dialogueMenu;
        
        //Name of charater
        public string? characterName { get; protected set; }

        //Unlock
        bool unlock = false;
        private const string more = "Keep talking with the character";
        private const string stop = "Stop if you have heard enough, and wish to move on";
        private const string who = "Learn more about how the character can help";
        //private const string items = "Ask what items you can unlock and how"; elder
        //private const string unlocked = "Unlock item"; elder
    }

    public sealed class Elder : Character
    {
 //       string currentLocation = ElderHouse;
        /*string characterName = "Elder";
        string characterPicture = "Picture of elder";
        string more = "Thank you for for listening Mayor, Let me tell you about how everything changed for the worse " +
            "for everyone in the village. \nWhen I was just a child 60 years ago the village was thriving. "
            + "\nNow we are just trying to survive. Our health i getting worse for everyday, \nbecause we either don't "
            + "get anything to eat or because the fish we eat are contaminated with plastic or other chemicals. "
            + "\nCompanies take our fish so we barely have enough food and we have to be careful deciding what fish to catch. "
            + "\nThey pollute our water and take our fish. They are slowly moving away from our area, "
            + "\nbut now we need to think about what fish we catch and eat fish from polluted water. "
            + "\nWe need you, Mayor. Please help the village become sustainable and make it thrive again.";
        string stop = "No problem";
        string who = "You can unlock algae cleaner and water filter and get it from the Elder, when unlocked. "
            + "\nYou need to increase population health to more than 90 to unlock algae cleaner, then talk to the elder to get it.";
        */
        public Elder()
        {
            characterName = "elder";
        }
    }

    public sealed class Scientist : Character
    {
//        string currentLocation = wastePlant;
        /*string characterName = "Scientist";
        string characterPicture = "Picture of Scientist";
*/
        public Scientist()
        {
            characterName = "Scientist";
        }

    }

    public sealed class Npc : Character
    {

    }
}