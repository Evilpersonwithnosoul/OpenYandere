using UnityEngine;
using OpenYandere.Managers;

namespace OpenYandere.Characters.Player
{
	public class PlayerCamera : MonoBehaviour
	{
		private PlayerMovement _playerMovement;
		
		private Vector3 _targetHeightOffset;
		
		private Vector3 _currentRotation;
		private Vector3 _currentRotationVelocity;

		public float HorizontalAxis { get; set; }
		public float VerticalAxis { get; set; }

		[Header("Target Settings:")]
		/* [SerializeField] */ private Transform _targetTransform;
		[SerializeField] private float _targetHeight = 1.65f;
		[SerializeField] private float _distanceFromTarget = 2f;
		
		[Header("Vertical (Up and Down) Settings:")]
		[SerializeField] private float _verticalMinLimit = -40f;
		[SerializeField] private float _verticalMaxLimit = 80f;
		
		[Header("Rotation Settings:")]
		[SerializeField] private float _rotationSmoothTime = 0.1f;
		
		[Header("Mouse Settings:")]
		[SerializeField] private float _mouseSensitivity = 5f;

		private void Awake()
		{
			PlayerManager playerManager = GameManager.Instance.PlayerManager;
			
			// Set player movement.
			_playerMovement = playerManager.PlayerMovement;
			
			// Set the player as the target
			_targetTransform = playerManager.Player.transform;
			_targetHeightOffset = new Vector3(0, _targetHeight, 0);
			
			// Set the axis to the current rotation of the target.
			HorizontalAxis = _targetTransform.eulerAngles.y;
			VerticalAxis = _targetTransform.eulerAngles.x;
		}

		private void LateUpdate()
		{
			HorizontalAxis += Input.GetAxis("Mouse X") * _mouseSensitivity;
			VerticalAxis -= Input.GetAxis("Mouse Y") * _mouseSensitivity;
			
			// Set the camera's horizontal axis.
			_playerMovement.SetCameraAxis(HorizontalAxis);
			
			// Clamp the vertical axis - looking up and down.
			VerticalAxis = Mathf.Clamp(VerticalAxis, _verticalMinLimit, _verticalMaxLimit);
			
			// Apply smoothing to the rotation.
			_currentRotation = Vector3.SmoothDamp(_currentRotation, new Vector3(VerticalAxis, HorizontalAxis), ref _currentRotationVelocity, _rotationSmoothTime);

			// The camera's rotation is the mouse's X and Y.
			transform.eulerAngles = _currentRotation;
			
			// The camera's position is the target position, minus the distance from the target, plus the target height.
			transform.position = _targetTransform.position - transform.forward * _distanceFromTarget + _targetHeightOffset;
		}
		
		public void SetTarget(Transform targetTransform, float targetHeight)
		{
			_targetTransform = targetTransform;
			_targetHeight = targetHeight;
			
			// Set the axis to the current rotation of the target.
			HorizontalAxis = _targetTransform.eulerAngles.y;
			VerticalAxis = _targetTransform.eulerAngles.x;
		}
	}
}
