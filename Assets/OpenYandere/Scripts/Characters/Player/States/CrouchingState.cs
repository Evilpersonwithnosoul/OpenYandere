using OpenYandere.Characters.Player.States.Traits;
using OpenYandere.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenYandere.Characters.Player.States
{
    internal class CrouchingState : IState
    {
        private PlayerManager _playerManager;
        
        public void Constructor(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public void Enter()
        {
            var playerMovement = _playerManager.PlayerMovement;
            // Supondo que a velocidade de agachamento seja metade da velocidade de caminhada
            playerMovement.SetMovementSpeed(playerMovement.WalkSpeed / 2);
        }

        public MovementState HandleInput(InputData input)
        {
            if (input.IsMoving && !input.IsCrouching) return MovementState.Walking;
            
            return MovementState.None;
        }

        public MovementState HandleUpdate(float deltaTime)
        {
            return MovementState.None;
        }
    }
}
