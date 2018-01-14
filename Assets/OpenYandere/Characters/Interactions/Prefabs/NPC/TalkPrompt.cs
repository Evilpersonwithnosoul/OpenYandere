using UnityEngine;

namespace OpenYandere.Characters.Interactions.Prefabs.NPC
{
    internal class TalkPrompt : Interactable
    {
        [SerializeField] private Characters.NPC.NPC _npc;
        [SerializeField] private Animator _dialogueBoxAnimator; // TODO: Remove this after testing is complete.
		
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
            
			// Testing showing the dialogue box.
			_dialogueBoxAnimator.SetBool("Visible", true);
			
			/*
			UIManager uiManager = GameManager.Instance.UIManager;

			// Populate the dialogue choices.
			uiManager.AddDialogueChoice("Compliment");
			uiManager.AddDialogueChoice("Gossip");
			uiManager.AddDialogueChoice("Perform a task");
			uiManager.AddDialogueChoice("Ask for favor");
			uiManager.AddDialogueChoice("Exit");
			
			// Set the dialogue text.
			uiManager.SetDialogueBox(_npc.Name, "Do you need something?");
			
			// Show dialogue box and choices.
			uiManager.ShowDialogueBox();
			uiManager.ShowDialogueChoices();
			*/
        }
    }
}