using UnityEngine;
using OpenYandere.Managers;

namespace OpenYandere.UI.Interactions
{
    internal class SquareOutline : MonoBehaviour
    {
        private Camera _playerCamera;
        
        [Header("References:")]
        [SerializeField] private RectTransform _rectTransform;
        
        private Transform _attachTransform;
        private Vector3 _offsetFromObject;
        
        private void Awake()
        {
            _playerCamera = GameManager.Instance.CameraManager.PlayerCamera.GetComponent<Camera>();
        }
        
        private void LateUpdate()
        {
            // Update the position of the square outline.
            _rectTransform.position = _playerCamera.WorldToScreenPoint(_attachTransform.position + _offsetFromObject);
        }

        public void Initialize(Transform attachTransform, Vector3 offsetFromObject)
        {
            _attachTransform = attachTransform;
            _offsetFromObject = offsetFromObject;
        }
    }
}