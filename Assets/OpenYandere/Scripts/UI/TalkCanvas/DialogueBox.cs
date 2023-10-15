using System;
using TMPro;
using UnityEngine;
using OpenYandere.Managers;
using OpenYandere.Characters;
using System.Collections.Generic;
using OpenYandere.Characters.NPC;
using OpenYandere.Characters.Player;
using TMPro;
namespace OpenYandere.UI.TalkCanvas
{
    public class DialogueBox : MonoBehaviour
    {
	    private struct DialogueEntry
	    {
		    public string CharacterName;
		    public string Text;
	    }
	    
	    private Player _player;
	    private NPC _interactingWithNPC;
	    
	    private bool _isVisible;
	    private bool _areChoicesVisible;

	    private readonly Queue<DialogueEntry> _dialogueEntries = new Queue<DialogueEntry>();
	    
	    [Header("References:")]
	    [SerializeField] private Animator _animator;
	    [SerializeField] private Animator _choicesAnimator;
	    [SerializeField] private TextMeshProUGUI _characterName, _dialogueText;
	    
	    private void Awake()
	    {
		    _player = GameManager.Instance.PlayerManager.Player.GetComponent<Player>();
	    }

	    private void Update()
	    {
		    if (!_isVisible) return;
			
		    if (Input.GetMouseButtonDown(0) && !_areChoicesVisible)
		    {
			    if (_dialogueEntries.Count > 0)
			    {
				    DialogueEntry dialogueEntry = _dialogueEntries.Dequeue();
					_characterName.SetText(dialogueEntry.CharacterName);
					_dialogueText.SetText(dialogueEntry.Text);// TODO: Animate the text.
			    }
			    else
			    {
				    _animator.SetBool("Visible", false);
				    _isVisible = false;
			    }
		    }
	    }

	    public void Initialise(NPC interactingWithNPC)
	    {
		    _interactingWithNPC = interactingWithNPC;
	    }

	    public void ShowBox()
	    {
		    _animator.SetBool("Visible", true);
		    _isVisible = true;
	    }
	    
        public void ShowChoices()
        {
	        _choicesAnimator.SetBool("Visible", true);
	        _areChoicesVisible = true;
        }

	    public void HideChoices()
	    {
		    _choicesAnimator.SetBool("Visible", false);
		    _areChoicesVisible = false;
	    }
	    
	    public void Queue(string characterName, string dialogueText)
	    {
		    _dialogueEntries.Enqueue(new DialogueEntry
		    {
			    CharacterName = characterName,
			    Text = dialogueText
		    });
	    }

	    public void SetText(string characterName, string dialogueText)
	    {
		    _characterName.SetText(characterName);
		    _dialogueText.SetText(dialogueText);
	    }
        
        public void OnComplimentButtonClicked()
        {
	        HideChoices();
	        
	        SetText(_player.Name, "I just wanted to tell you that you look lovely today!");

	        if (_player.Reputation >= 50)
	        {
		        Queue(_interactingWithNPC.Name, "Wow! That means a lot coming from you! Thank you so much!");
	        }
	        else if(_player.Reputation < 0)
	        {
		        Queue(_interactingWithNPC.Name, "Umm...thanks, I guess...?...");
	        }
	        else
	        {
		        Queue(_interactingWithNPC.Name, "Really? That's so nice of you to say!");
	        }

	        _player.Reputation += 1;
        }
		
        public void OnGossipButtonClicked()
        {
	        HideChoices();
			// TODO
        }
		
        public void OnPerformTaskButtonClicked()
        {
	        HideChoices();
	        // TODO
        }
		
        public void OnAskForFavorButtonClicked()
        {
	        HideChoices();
	        // TODO
        }
		
        public void OnExitButtonClicked()
        {
	        HideChoices();
	        // TODO
        }
    }
}