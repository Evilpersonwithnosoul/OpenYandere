using UnityEngine;

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
            // TODO
        }
    }
}