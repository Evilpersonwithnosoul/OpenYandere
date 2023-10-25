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
            if (_npc.IsAlive)
            {
                PromptText = "Talk";
                PromptKeyCode = KeyCode.E;
                OffsetFromObject = new Vector3(0, 0.35f, 0);
            }

        }

        protected override void OnPromptTriggered()
        {
            if (_npc.IsAlive)
            {
                Debug.Log("Talking to " + _npc.characterName);

                DialogueBox dialogueBox = GameManager.Instance.UIManager.DialogueBox;

                // Initialise the dialogue box.
                dialogueBox.Initialise(_npc);

                // Set the dialogue text.
                dialogueBox.SetText(_npc.characterName, "Do you need something?");

                // Show dialogue box and choices.
                dialogueBox.ShowBox();
                dialogueBox.ShowChoices();
            }

        }
    }
}