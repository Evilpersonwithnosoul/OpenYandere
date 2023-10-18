using OpenYandere.Managers.Traits;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace OpenYandere.Characters.Player
{
    [RequireComponent(typeof(CharacterAnimator), typeof(CharacterController), typeof(NavMeshObstacle))]
    public class PlayerMovement : MonoBehaviour
    {
        private MovementStateMachine _movementStateMachine;
        
        private InputData _inputData;
        private AnimatorData _animatorData;
        
        private CharacterController _characterController;
        private CharacterAnimator _characterAnimator;
        
        private float _movementSpeed, _cameraHorizontalAxis;
        
        [Header("Movement Settings:")]
        [Tooltip("The walking speed of the player.")]
        public float WalkSpeed = 2.0f;
        [Tooltip("The running speed of the player.")]
        public float RunSpeed = 6.0f;

        private bool _canMove = true;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _characterAnimator = GetComponent<CharacterAnimator>();
        }
        
        private void FixedUpdate()
        {
            if (_canMove)
            {
                HandleMovement();
            }
        }
        protected void HandleMovement()
        {
            var horizontalAxis = Input.GetAxis("Horizontal");
            var verticalAxis = Input.GetAxis("Vertical");

            // Set the input data.
            _inputData.IsMoving = true;
            _inputData.IsRunning = Input.GetKey(KeyCode.LeftShift);

            // If the player is moving on either axis.
            if (Math.Abs(horizontalAxis) > 0f || Math.Abs(verticalAxis) > 0f)
            {
                // Get the direction to move in.
                var moveDirection = new Vector3(horizontalAxis, 0, verticalAxis) * _movementSpeed;
                moveDirection = Quaternion.AngleAxis(_cameraHorizontalAxis, Vector3.up) * moveDirection;

                // Update the animator entry.
                _animatorData.IsRunning = _inputData.IsRunning;
                _animatorData.MoveDirection = moveDirection;

                // Make the player look in the direction we are moving towards.
                transform.rotation = Quaternion.LookRotation(moveDirection);

                // Move in that direction.
                _characterController.Move(moveDirection * Time.deltaTime);
            }
            else
            {
                // Update the input data.
                _inputData.IsMoving = false;
                _inputData.IsRunning = false;

                // Update the animator data.
                _animatorData.IsRunning = false;
                _animatorData.MoveDirection = Vector3.zero;
            }

            // Update the character animator's data.
            _characterAnimator.UpdateData(_animatorData);

            // Have the current state handle the changes in input.
            _movementStateMachine.HandleInput(_inputData);

            // Update the current state.
            _movementStateMachine.Update(Time.deltaTime);
        }
        public void BlockMovement()
        {
            _canMove = false;
            _inputData.IsMoving = false;
            _inputData.IsRunning = false;

            // Certificando-se de que os valores do animator são atualizados.
            _animatorData.IsRunning = false;
            _animatorData.MoveDirection = Vector3.zero;
            _characterAnimator.UpdateData(_animatorData);
        }

        public void UnblockMovement()
        {
            _canMove = true;
        }
        public void Constructor(MovementStateMachine movementStateMachine) =>
            _movementStateMachine = movementStateMachine;
        
        public void SetMovementSpeed(float speed) => _movementSpeed = speed;

        public void SetCameraAxis(float horizontalAxis) => _cameraHorizontalAxis = horizontalAxis;
    }
}