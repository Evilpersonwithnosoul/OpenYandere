using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using OpenYandere.Managers;
using TMPro;
namespace OpenYandere.UI.Interactions
{
    internal class RadialPrompt : MonoBehaviour
    {
        private Camera _playerCamera;
        
        private KeyCode _promptKeyCode;

        private const float HoldDuration = 0.75f;
        private float _currentHeldTime;
        
        private Transform _attachTransform;
        private Vector3 _offsetFromObject;
        
        public Action OnPromptTriggered;
        
        [Header("References:")]
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _loadingBar;        
        [SerializeField] private TextMeshProUGUI _promptCharacter, _promptText;
        
        private void Awake()
        {           
            _playerCamera = GameManager.Instance.CameraManager.PlayerCamera.GetComponent<Camera>();
        }

        private void Update()
        {
            // If the user is holding the key down.
            if (Input.GetKey(_promptKeyCode))
            {
                // Add the white fill to the loading bar.
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (_loadingBar.fillAmount == 0f) _loadingBar.fillAmount = 1f;
                
                // Update how long it has been held.
                _currentHeldTime += Time.deltaTime;
                
                // Start decreasing the while fill.
                _loadingBar.fillAmount = 1f - _currentHeldTime / HoldDuration;
                
                // If it hasn't been held for long enough yet, return.
                if (!(_currentHeldTime >= HoldDuration)) return;
                
                // Reset the held time.
                _currentHeldTime = 0f;
                
                //Reset the fill amount.
                _loadingBar.fillAmount = 0f;
                
                //Call the function.
                OnPromptTriggered?.Invoke();
            }
            // If the user held the key then stopped, the fill amount would be more than
            // zero, so we need to reset the variables.
            else if (_loadingBar.fillAmount > 0f)
            {
                _currentHeldTime = 0f;
                _loadingBar.fillAmount = 0f;
            }
        }

        private void LateUpdate()
        {
            // Update the position of the radial prompt.
            _rectTransform.position = _playerCamera.WorldToScreenPoint(_attachTransform.position + _offsetFromObject);
        }

        public void Initialize(string promptText, KeyCode promptKeyCode, Transform attachTransform, Vector3 offsetFromObject, Action onPromptTriggered)
        {
            _promptText.text = promptText;
            _promptCharacter.text = promptKeyCode.ToString();
            _promptKeyCode = promptKeyCode;

            _attachTransform = attachTransform;
            _offsetFromObject = offsetFromObject;

            OnPromptTriggered = onPromptTriggered;
        }
    }
}