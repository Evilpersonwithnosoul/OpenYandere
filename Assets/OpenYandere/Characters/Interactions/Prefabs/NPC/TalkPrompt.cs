using UnityEngine;
using OpenYandere.Managers;
using OpenYandere.UI.TalkCanvas;

namespace OpenYandere.Characters.Interactions.Prefabs.NPC
{
    internal class TalkPrompt : Interactable
    {
        [SerializeField] private Characters.NPC.NPC _npc;
		
        protected override void Awake()
        {
            base.Awake();
			
            PromptText = "Talk";
            PromptKeyCode = KeyCode.E;
            OffsetFromObject = new Vector3(0, 0.35f, 0);
        }

        protected override void OnPromptTriggered()
        {
            Debug.Log("Talking to " + _npc.Name);
            
	        DialogueBox dialogueBox = GameManager.Instance.UIManager.DialogueBox;
	        
	        // Initialise the dialogue box.
	        dialogueBox.Initialise(_npc);
	        
	        // Set the dialogue text.
	        dialogueBox.SetText(_npc.Name, "Do you need something?");
			
	        // Show dialogue box and choices.
	        dialogueBox.ShowBox();
	        dialogueBox.ShowChoices();
        }
    }
}