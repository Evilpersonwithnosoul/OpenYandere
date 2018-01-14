using System;
using UnityEngine;
using UnityEngine.AI;

namespace OpenYandere.Characters.NPC
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NPCMovement : MonoBehaviour
    {
        private AnimatorData _animatorData;
        
        public NavMeshAgent NavigationAgent => _navMeshAgent;

        [Header("References:")]
        [SerializeField] private CharacterAnimator _characterAnimator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        [Header("Settings:")]
        [Tooltip("The walking speed of the NPC.")]
        public float WalkSpeed = 2.0f;
        [Tooltip("The running speed of the NPC.")]
        public float RunSpeed = 6.0f;
        [Tooltip("Is the NPC running?")]
        public bool IsRunning;
        
        private void Awake()
        {
            _navMeshAgent.updateRotation = true;
            _navMeshAgent.updatePosition = true;
        }
        
        private void Update()
        {
            // Get the current velocity of the navigation agent.
            var horizontalAxis = _navMeshAgent.velocity.x;
            var verticalAxis = _navMeshAgent.velocity.z;
            
            // Check if either axis is more than zero - meaning the NPC is moving.
            if (Math.Abs(horizontalAxis) > 0f || Math.Abs(verticalAxis) > 0f)
            {
                // Get the speed to move at.
                var movementSpeed = IsRunning ? RunSpeed : WalkSpeed;
                
                // Calculate the direction to move in.
                var moveDirection = new Vector3(horizontalAxis, 0, verticalAxis);
                
                // Update the speed of the navigation agent.
                _navMeshAgent.speed = movementSpeed;
                
                // Update the animator entry.
                _animatorData.IsRunning = IsRunning;
                _animatorData.MoveDirection = moveDirection;
            }
            else
            {
                // Update the animator data.
                _animatorData.IsRunning = false;
                _animatorData.MoveDirection = Vector3.zero;
            }
            
            // Update the data the character animator is using.
            _characterAnimator.UpdateData(_animatorData);
        }

        public void Resume()
        {
            // Resume navigation agent.
            _navMeshAgent.isStopped = false;
            
            // Resume the animator.
            _characterAnimator.Resume();
        }

        public void Pause()
        {
            // Pause the navigation agent.
            _navMeshAgent.isStopped = true;
            
            // Pause the animator.
            _characterAnimator.Pause();
        }
    }
}