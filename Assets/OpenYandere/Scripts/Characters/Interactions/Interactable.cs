using UnityEngine;
using OpenYandere.Managers;

namespace OpenYandere.Characters.Interactions
{
    internal abstract class Interactable : MonoBehaviour
    {
        private UIManager _uiManager;
        private PlayerManager _playerManager;
        [SerializeField] private Transform headIKTarget;

        private GameObject _radialPrompt, _squareOutline;
        
        protected string PromptText;
        protected KeyCode PromptKeyCode;
        protected Vector3 OffsetFromObject = Vector3.zero;
        
        private bool _isPlayerInside;
        
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Transform _attachTransform;

        protected Vector3 originalIKTargetPosition = new(0, 1.5f,1);

        protected virtual void Awake()
        {
            originalIKTargetPosition = headIKTarget.position;
            _uiManager = GameManager.Instance.UIManager;
            _playerManager = GameManager.Instance.PlayerManager;
            if (headIKTarget != null)
            {
                originalIKTargetPosition = headIKTarget.position;
            }
        }
        
        private void Update()
        {
            // If the player is not inside the interaction radius return.
            if (!_isPlayerInside) return;
            
            // If the mesh is currently being rendered by the camera, attempt
            // to show a radial prompt.
            if (_renderer.isVisible)
            {
                ShowRadialPrompt();
      
            }
            // Otherwise, hide the square outline and radial prompt.
            else
            {
                ResetLook();
                HideRadialPrompt();
                HideSquareOutline();
            }
        }
        protected void LookAtObject()
        {
            if (headIKTarget == null) return;
            // Defina uma posição alvo baseada na posição do objeto e um pequeno offset para ajuste
            Vector3 targetPosition = _attachTransform.position;

            // Mova o IK Target suavemente para a posição alvo
            headIKTarget.position = Vector3.Lerp(headIKTarget.position, targetPosition, Time.deltaTime * 2); // Ajuste a velocidade conforme necessário
        }

        protected void ResetLook()
        {
            if (headIKTarget == null) return;
            // Retorna o IK Target para sua posição original ou qualquer posição neutra que você desejar
            headIKTarget.position = Vector3.Lerp(headIKTarget.position, originalIKTargetPosition, Time.deltaTime * 5); // Ajuste a velocidade conforme necessário
        }

        protected void OnTriggerEnter(Collider other)
        {
            // If the tag is not equal to 'Player' return.
            if (!other.CompareTag("Player")) return;
            LookAtObject();
            // The player has entered the interaction radius, update the flag.
            _isPlayerInside = true;
        }

        protected void OnTriggerExit(Collider other)
        {
            // If the tag is not equal to 'Player' return.
            if (!other.CompareTag("Player")) return;
			
            // The player has exited the interaction radius, update the flag.
            _isPlayerInside = false;
			
            // Hide the radial prompt and square outline.
            HideRadialPrompt();
            HideSquareOutline();
            ResetLook();
        }

        protected void ShowRadialPrompt()
        {
            // If this interactable already has a radial prompt, return.
            if (_radialPrompt != null) return; 
            
            // Attempt to get a radial prompt.
            _radialPrompt = _uiManager.GetRadialPrompt(this, PromptText, PromptKeyCode, _attachTransform, OffsetFromObject, OnPromptTriggered);

            if (_radialPrompt != null)
            {
                // Hide the square outline.
                HideSquareOutline();

                // Show the radial prompt.
                _radialPrompt.SetActive(true);
            }
            else
            {
                var registeredInteractable = _uiManager.GetInteractable(PromptKeyCode);            
                var playerPosition = _playerManager.Player.transform.position;
            
                // The distance from the player to this interactable.
                var distanceFromPlayerToThis = Vector3.Distance(playerPosition, transform.position);
            
                // The distance from the player to the registed interactable.
                var distanceFromPlayerToRegistered = Vector3.Distance(playerPosition, registeredInteractable.transform.position);

                var sqrDistFromPlayerToThis = (playerPosition - transform.position).sqrMagnitude;
                var sqrDistFromPlayerToRegistered = (playerPosition - registeredInteractable.transform.position).sqrMagnitude;
                if (sqrDistFromPlayerToThis > sqrDistFromPlayerToRegistered)
                {
                    // Switch the registered interactable to a square outline.
                    registeredInteractable.ToSquareOutline();

                    // Attempt to show the radial prompt again.
                    ShowRadialPrompt();
                }
                else
                {
                    // This interactable should show a square outline.
                    ToSquareOutline();
                }
            }
        }

        protected void HideRadialPrompt()
        {
            // If this interactable has no radial prompt, return.
            if (_radialPrompt == null) return;
            
            // Release the radial prompt.
            _uiManager.ReleaseRadialPrompt(PromptKeyCode, _radialPrompt);
            _radialPrompt = null;
        }

        private void ToSquareOutline()
        {
            // If this interactable already has a square outline, return.
            if(_squareOutline != null) return;
		    
            // Hide the radial prompt.
            HideRadialPrompt();
		    
            // Get a square outline.
            _squareOutline = _uiManager.GetSquareOutline(_attachTransform, OffsetFromObject);
		    
            // Show a square outline.
            _squareOutline.SetActive(true);
        }

        private void HideSquareOutline()
        {
            // If this interactable has no square outline, return.
            if (_squareOutline == null) return;
            
            // Release the square outline.
            _squareOutline.SetActive(false);
            _squareOutline = null;
        }
		
        protected abstract void OnPromptTriggered();
    }
}