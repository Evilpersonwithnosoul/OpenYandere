using System;
using UnityEngine;
using System.Collections.Generic;
using OpenYandere.UI.Interactions;
using OpenYandere.Characters.Interactions;

namespace OpenYandere.Managers
{
    internal class UIManager : MonoBehaviour
    {
        private ObjectPoolManager _objectPoolManager;
        
        private readonly Dictionary<KeyCode, Interactable> _registeredInteractables = new Dictionary<KeyCode, Interactable>();
		
		private void Awake()
		{
			_objectPoolManager = GameManager.Instance.ObjectPoolManager;
		}
		
		public GameObject GetRadialPrompt(Interactable interactable, string promptText, KeyCode promptKeyCode,
			Transform attachTransform, Vector3 offsetFromObject, Action onPromptTriggered)
		{
			// If a prompt is already using the same key, return null.
			if (_registeredInteractables.ContainsKey(promptKeyCode)) return null;
            
			// Attempt to get a radial prompt from the pool.
			var radialPromptObject = _objectPoolManager["Radial Prompts"];
			
			// If it failed to get a radial prompt, return null.
			if (radialPromptObject == null) return null;
            
			// Initialise the radial prompt.
			var radialPrompt = radialPromptObject.GetComponent<RadialPrompt>();
			radialPrompt.Initialize(promptText, promptKeyCode, attachTransform, offsetFromObject, onPromptTriggered);
            
			// Associate the key code with the prompt. 
			_registeredInteractables.Add(promptKeyCode, interactable);
                
			return radialPromptObject;
		}
		
		public void ReleaseRadialPrompt(KeyCode promptKeyCode, GameObject radialPromptObject)
		{
			_registeredInteractables.Remove(promptKeyCode);
			radialPromptObject.SetActive(false);
		}

		public GameObject GetSquareOutline(Transform attachTransform, Vector3 offsetFromObject)
		{
			// Get a square outline from the pool.
			var squareOutlineObject = _objectPoolManager["Square Outlines"];
			
			// Initialise the square outline.
			var squareOutline = squareOutlineObject.GetComponent<SquareOutline>();
			squareOutline.Initialize(attachTransform, offsetFromObject);

			return squareOutlineObject;
		}
		
		public Interactable GetInteractable(KeyCode keyCode)
		{
			// If the key code is registered, return the interactable. Otherwise, return null.
			return _registeredInteractables.ContainsKey(keyCode) ? _registeredInteractables[keyCode] : null;
		}
    }
}