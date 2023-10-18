using UnityEngine;

namespace OpenYandere.Characters
{
    [CreateAssetMenu(fileName = "newStudentPersonality", menuName = "Student/Personality")]
    public class SOStudentPersonality : ScriptableObject
    {
        public PersonalityType personalityType;
        public SOAnimationSet animationSet;
        public Club club;
        public float homeTime = 0f;
    }
    public enum PersonalityType
    {
        Normal, Nerdy, Gossipy, Shy, Bold
    };
    public enum Club { None, Cooking, Art, Games, Library, Books, Movies };
}
