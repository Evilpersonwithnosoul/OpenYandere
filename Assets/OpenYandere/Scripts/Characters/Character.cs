using UnityEngine;

namespace OpenYandere.Characters
{
	[RequireComponent(typeof(CharacterAnimator))]
	public abstract class Character : MonoBehaviour
	{
		public enum GenderType { Male, Female }
		public enum ClubType { None }
		
		public int Id;
		public string Name;
		public GenderType Gender;
		public ClubType Club;
		public bool IsAlive = true;
	}
}

