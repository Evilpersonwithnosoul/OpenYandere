using OpenYandere.Characters.Interactions;
using OpenYandere.Managers;
using OpenYandere.UI.TalkCanvas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenYandere.Characters.Interactions.Prefabs.NPC
{
    internal class DragPrompt : Interactable
    {
        [SerializeField] private Characters.NPC.NPC _npc;

        protected override void Awake()
        {
            base.Awake();
            if (!_npc.IsAlive)
            {
                PromptText = "Drag";
                PromptKeyCode = KeyCode.E;
                OffsetFromObject = new Vector3(0, 0.35f, 0);
            }

        }
        protected override void OnPromptTriggered()
        {
            if (!_npc.IsAlive)
            {
                Debug.Log("Dragging " + _npc.characterName);

            }

        }
    }

}
